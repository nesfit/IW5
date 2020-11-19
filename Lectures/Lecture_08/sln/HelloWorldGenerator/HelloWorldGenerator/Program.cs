using dnlib;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.IO;
using System.Linq;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create assembly and main module
            var assembly = new AssemblyDefUser("HelloWorld");
            var module = new ModuleDefUser("HelloWorld.dll", null, new AssemblyRefUser("System.Runtime", new Version(4, 2, 2, 0)));
            module.RuntimeVersion = dnlib.DotNet.MD.MDHeaderRuntimeVersion.MS_CLR_40;

            // Create type Program
            var type = new TypeDefUser(
                @namespace: "HelloWorld",
                name: "Program",
                baseType: module.CorLibTypes.Object.TypeDefOrRef);
            // Set class attributes
            type.Attributes = TypeAttributes.Public | TypeAttributes.Class;

            // Create method Main
            var method = new MethodDefUser(
                name: "Main",
                methodSig: MethodSig.CreateStatic(
                    retType: module.CorLibTypes.Void,
                    argType1: new SZArraySig(module.CorLibTypes.String)));
            // Name parameter
            method.ParamDefs.Add(new ParamDefUser("args", 1));
            // Set method attributes
            method.Attributes = MethodAttributes.Public | MethodAttributes.Static;
            // Set attributes for method's implementation
            method.ImplAttributes = MethodImplAttributes.IL | MethodImplAttributes.Managed;



            #region IMPLEMENTATION
            // Create reference to System.Console
            var consoleRef = new TypeRefUser(module, "System", "Console", new AssemblyRefUser("System.Console"));
            // Create reference to System.Console::WriteLine(string)
            var writeLineRef = new MemberRefUser(module, "WriteLine",
                MethodSig.CreateStatic(
                    retType: module.CorLibTypes.Void,
                    argType1: module.CorLibTypes.String), 
                consoleRef);

            // Implement body of the Program::Main(string[]) method
            var body = new CilBody();
            body.Instructions.Add(Instruction.Create(OpCodes.Ldstr, "Hello World!"));
            body.Instructions.Add(Instruction.Create(OpCodes.Call, writeLineRef));
            body.Instructions.Add(Instruction.Create(OpCodes.Br, body.Instructions.First()));
            body.Instructions.Add(Instruction.Create(OpCodes.Ret));

            method.Body = body;
            #endregion



            #region ASSEMBLE
            // Wire-it up!
            assembly.Modules.Add(module);
            module.Types.Add(type);
            type.Methods.Add(method);

            // Set entrypoint
            module.EntryPoint = method;

            // Store assembly
            module.Write(Path.Combine("Generated", "HelloWorld.dll"), new dnlib.DotNet.Writer.ModuleWriterOptions(module));
            #endregion
        }
    }
}
