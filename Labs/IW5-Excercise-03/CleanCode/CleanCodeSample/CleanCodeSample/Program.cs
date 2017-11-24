using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var mathExcercisesProgram = new MathExcercisesProgram();
            mathExcercisesProgram.Run();

            Console.ReadKey();
        }
    }
}
