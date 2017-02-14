using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

namespace AdvancedConstructsInCsharp
{
  public class ExceptionExamples
  {
    private StreamReader _file;

    public static void RunExamples()
    {
      var exceptionExamples = new ExceptionExamples();
      Console.WriteLine("--- Exception Examples ---");
     
      //Example 6 - Central exception handling
      //AppDomain.CurrentDomain.FirstChanceException += CurrentDomainFirstChanceException; ;
      //AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

      //Example 1 - Will pass through
      exceptionExamples.ReadNumber("FileWithByteValue.txt");
      Console.ReadKey(); 

      //Example 2 - Unhandled exception will crash app
      exceptionExamples.ReadNumber("FileWithStringValue.txt");
      Console.ReadKey();

      //Example 3 - Specifically handled FormatException
      exceptionExamples.ReadNumberWithErrorHandling("FileWithStringValue.txt");
      Console.ReadKey();

      //Example 4 - Generically handled FileNotFoundException 
      exceptionExamples.ReadNumberWithErrorHandling("ThisFileCertainlyDoesNotExit");
      Console.ReadKey();

      //Example 5 - Method precondition / argument check  
      exceptionExamples.GetHashCodeForObject(null);
      Console.ReadKey();
    }

    static void CurrentDomainFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
    {
      Console.WriteLine("CENTRAL HANDLER - First Chance exception occured!");
    }

    static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      Console.WriteLine("CENTRAL HANDLER - Unhandled exception occured!");
    }

    public void ReadNumber(string fileName)
    {
      Console.WriteLine("Reading number from file: " + fileName);
      _file = new StreamReader(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None));
      int number = Int32.Parse(_file.ReadLine());
      Console.WriteLine("Number is: " + number);
    }


    public void ReadNumberWithErrorHandling(string fileName)
    {
      try
      {
        ReadNumber(fileName);
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
        //throw;

        //Throws new exception. Original exception is suplied as InternalException parameter
        //throw new Exception("Unable to read from file", excp);

        //Throws new Custom exception. Original exception is suplied as InternalException parameter
        //throw new CustomException("Unable to read from file", excp);
      }
      finally
      {
        //Ensures that the file is always closed
        if (_file != null)
        {
          _file.Dispose();
          _file = null;
        }
      }
    }


    public int GetHashCodeForObject(object obj)
    {
      if (obj == null)
      {
        throw new ArgumentNullException("obj", "The supplied object can't be null");
      }
      return obj.GetHashCode();
    }
  }

  public class CustomException : Exception
  {
    public CustomException()
    {
    }

    public CustomException(string message)
      : base(message)
    {
    }

    public CustomException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    protected CustomException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }

}
