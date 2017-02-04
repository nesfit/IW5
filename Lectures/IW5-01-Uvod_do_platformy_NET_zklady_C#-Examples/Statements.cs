using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace FirstProgram
{
    [TestFixture]
    internal class Statements
    {
        public void LocalDeclaration()
        {
            //int x;
            //{
            //    int y;
            //    int x; // Error - x already defined
            //}
            //{
            //    int y; // OK - y not in scope
            //}
            //Console.WriteLine(y); // Error - y is out of scope
        }

        private void IfStatement()
        {
            if (5 < 2 * 3)
                Console.WriteLine("true"); // true

            if (5 < 2 * 3)
            {
                Console.WriteLine("true");
                Console.WriteLine("Let's move on!");
            }
            else
            {
                Console.WriteLine("False"); // False        
            }

            if (2 + 2 == 5)
                Console.WriteLine("Does not compute");
            Console.WriteLine("False"); // False
        }

        private void SwitchStatement(int cardNumber)
        {
            switch (cardNumber)
            {
                case 13:
                    Console.WriteLine("King");
                    break;
                case 12:
                    Console.WriteLine("Queen");
                    break;
                case 11:
                    Console.WriteLine("Jack");
                    break;
                case -1: // Joker is −1
                    goto case 12; // In this game joker counts as queen
                default: // Executes for any other cardNumber
                    Console.WriteLine(cardNumber);
                    break;
            }
        }

        private void Throw(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
        }

        private void Goto()
        {
            var i = 1;
            startLoop:
            if (i <= 5)
            {
                Console.Write(i + " ");
                i++;
                goto startLoop;
            }
        }

        private void Continue()
        {
            for (var i = 0; i < 10; i++)
            {
                if (i % 2 == 0) // If i is even,
                    continue; // continue with next iteration
                Console.Write(i + " ");
            }
        }

        private void Break()
        {
            var x = 0;
            while (true)
                if (x++ > 5)
                    break; // break from the loop
            // execution continues here after break
        }

        private void Foreach()
        {
            foreach (var c in "beer") // c is the iteration variable
                Console.WriteLine(c);
        }

        private void For()
        {
            for (int i = 0, prevFib = 1, curFib = 1; i < 10; i++)
            {
                Console.WriteLine(prevFib);
                var newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }

        private void DoWhile()
        {
            var i = 0;
            do
            {
                Console.WriteLine(i);
                i++;
            } while (i < 3);
        }

        private void While()
        {
            var i = 0;
            while (i < 3)
            {
                Console.WriteLine(i);
                i++;
            }
        }

        private decimal Return(decimal d)
        {
            var p = d * 100m;
            return p; // Return to the calling method with value
        }

        [Test]
        public void ExpressionStatement()
        {
            // Declare variables with declaration statements:
            string s;
            int x, y;
            StringBuilder sb;

            x = 1 + 2; // Assignment expression
            x++; // Increment expression
            y = Math.Max(x, 5); // Assignment expression
            Console.WriteLine(y); // Method call expression
            sb = new StringBuilder(); // Assignment expression
            new StringBuilder(); // Object instantiation expression

            new StringBuilder(); // Legal, but useless
            new string('c', 3); // Legal, but useless
            x.Equals(y); // Legal, but useless
        }

        [Test]
        public void IterationStatements()
        {
            While();
            DoWhile();
            For();
            Foreach();
        }

        [Test]
        public void JumpStatements()
        {
            Break();
            Continue();
            Goto();
            Return(5);
            try
            {
                Throw(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Exception {ex.GetType().Name} handeling...");
            }
        }

        [Test]
        public void MiscellaneousStatements()
        {
            byte[] buffer = {};
            var offset = 0;
            var count = 0;
            try
            {
                using (var file = File.Open(@"c.\Filepath.txt", FileMode.OpenOrCreate))
                {
                    file.Write(buffer, offset, count);
                } // file.Dispose() is called here
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("You should probably check existence of file before opening it...");
            }
        }

        [Test]
        public void SelectionStatements()
        {
            IfStatement();

            SwitchStatement(5);
        }
    }
}