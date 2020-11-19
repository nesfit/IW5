using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWeaver
{
    static class MethodDefExtensions
    {
        private static IEnumerable<T> LazyListReverse<T>(this List<T> list)
        {
            for (var i = list.Count - 1; i >= 0; i--)
                yield return list[i];
        }

        public static void InjectBefore(this MethodDef method, Instruction instruction, List<Instruction> toInject)
        {
            var index = method.Body.Instructions.IndexOf(instruction);
            foreach (var instr in toInject.LazyListReverse())
                method.Body.Instructions.Insert(index, instr);
        }
    }
}
