using System;
using System.IO;
using System.Runtime.ExceptionServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedConstructsInCsharp.Exceptions
{
    [TestClass]
    public class ExceptionExamples
    {
        public TestContext TestContext { get; set; }

        private StreamReader _file;

        [ClassInitialize]
        public static void ClassSetup(TestContext context)
        {
            //Example 1 - Central exception handling
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomainFirstChanceException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
            string fileName = context.TestDeploymentDir + @"\FileWithStringValue.txt";
            File.WriteAllText(fileName, "fourtytwo");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ReleaseFile(); // needs to release the file manually
        }

        private static void CurrentDomainFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            Console.WriteLine("CENTRAL HANDLER - First Chance exception occured!");
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("CENTRAL HANDLER - Unhandled exception occured!");
        }

        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void FileRemainsOpen()
        {
            this.ReadNumber("FileWithStringValue.txt");
        }
        
        [TestMethod]
        public void Specifically_handled_FormatException()
        {
            ReadNumberWithErrorHandling("FileWithStringValue.txt");
        }

        [ExpectedException(typeof(FileNotFoundException))]
        [TestMethod]
        public void Generically_handled_FileNotFoundException()
        {
            ReadNumberWithErrorHandling("ThisFileCertainlyDoesNotExit");
        }

        private void ReadNumberWithErrorHandling(string fileName)
        {
            try
            {
                this.ReadNumber(fileName);
            }
            catch (FormatException) //Handles specific exception type - FormatException
            {
                Console.WriteLine("File does not contain number!");
            }
            catch (Exception excp) //Handles any other exception
            {
                //This case consumes the exception
                Console.WriteLine("Can't read number. Error occured:");
                Console.WriteLine(excp);

                //Retrows the original exception 
                throw;

                //Throws new exception. Original exception is suplied as InternalException parameter
                throw new Exception("Unable to read from file", excp);

                //Throws new Custom exception. Original exception is suplied as InternalException parameter
                throw new CustomException("Unable to read from file", excp);
            }
            finally
            {
                //Ensures that the file is always closed
                ReleaseFile();
            }
        }

        private void ReleaseFile()
        {
            if (this._file != null)
            {
                this._file.Dispose();
                this._file = null;
            }
        }

        private void ReadNumber(string fileName)
        {
            Console.WriteLine("Reading number from file: " + fileName);
            string fullFileName = this.TestContext.TestDeploymentDir + @"\" + fileName;
            this._file = new StreamReader(File.Open(fullFileName, FileMode.Open, FileAccess.Read, FileShare.None));
            int number = Int32.Parse(this._file.ReadLine());
            Console.WriteLine("Number is: " + number);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void ArgumentChecks()
        {
            GetHashCodeForObject(null);
        }

        private static int GetHashCodeForObject(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "The supplied object can't be null");
            }
            return obj.GetHashCode();
        }
    }
}
