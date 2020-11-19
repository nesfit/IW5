using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MyLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleWeaver
{
    class Program
    {
        static MemberRef MethodEnterRef;
        static MemberRef MethodReturnRef;

        /// <summary>
        /// Hijack current thread and give control to the logging library at the beginning of a method
        /// </summary>
        /// <param name="method"></param>
        static void InjectMethodEnterHook(MethodDef method)
        {
            var prolog = new List<Instruction>()
            {
                Instruction.Create(OpCodes.Ldstr, method.FullName),
                Instruction.Create(OpCodes.Call, MethodEnterRef)
            };

            method.InjectBefore(method.Body.Instructions.First(), prolog);
        }
        
        /// <summary>
        /// Hijack current thread and give control to the logging library just before method return
        /// </summary>
        /// <param name="method">Method to hook</param>
        static void InjectMethodReturnHook(MethodDef method)
        {
            var epilog = new List<Instruction>()
            {
                Instruction.Create(OpCodes.Ldstr, method.FullName),
                Instruction.Create(OpCodes.Call, MethodReturnRef)
            };

            var toHook = method.Body.Instructions.Where(i => i.OpCode.Code == Code.Ret).ToArray();
            foreach (var instruction in toHook)
                method.InjectBefore(instruction, epilog);
        }

        static void Main(string[] args)
        {
            // Target application
            const string applicationPath = @"<your-path-to-target-assembly>\TestProgram.dll";
            var module = ModuleDefMD.Load(applicationPath);

            // Create reference to our logger library
            var loggerAssemblyRef = new AssemblyRefUser(typeof(Logger).Assembly.GetName());

            // Create reference to hooks
            var loggerTypeRef = new TypeRefUser(module, "MyLogger", nameof(Logger), loggerAssemblyRef);
            MethodEnterRef = new MemberRefUser(module, nameof(Logger.MethodEnter),
                MethodSig.CreateStatic(module.CorLibTypes.Void, module.CorLibTypes.String), loggerTypeRef);
            MethodReturnRef = new MemberRefUser(module, nameof(Logger.MethodLeave),
                MethodSig.CreateStatic(module.CorLibTypes.Void, module.CorLibTypes.String), loggerTypeRef);

            // Hook methods
            foreach (var type in module.Types ?? Enumerable.Empty<TypeDef>())
            {
                foreach (var method in type.Methods ?? Enumerable.Empty<MethodDef>().Where(m => m.HasBody))
                {
                    InjectMethodEnterHook(method);
                    InjectMethodReturnHook(method);
                }
            }

            // Store weaved application
            var filename = Path.GetFileNameWithoutExtension(applicationPath);
            var directory = Path.GetDirectoryName(applicationPath);
            module.Write(Path.Combine(directory, "Generated", $"{filename}.dll"));
        }
    }
}
