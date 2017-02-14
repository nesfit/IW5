using System;
using System.Linq.Expressions;

namespace AdvancedConstructsInCsharp
{
  public class LambdaExpressions
  {
    public static void RunExamples()
    {
      var lambdaExpressions = new LambdaExpressions();
      Console.WriteLine("\r\n\r\n--- Lambda Expression Examples ---");

      //Example 1 - Lambda creation
      lambdaExpressions.LambdaCreation();
      Console.ReadKey();

      //Example 2 - Capturing outer variable
      lambdaExpressions.VariableCapture();
      Console.ReadKey();

      //Example 3 - Variable Update
      lambdaExpressions.UpdateOfLocalVaribale();
      Console.ReadKey();

      //Example 4 - Capture of for loop variable
      lambdaExpressions.ForLoopVariableCapture();
      Console.ReadKey();

      //Example 5 - Capture of for loop variable copied to local
      lambdaExpressions.ForLoopVariableCaptureWithLocalVar();
      Console.ReadKey();

      //Example 6 - Capture of foreach variable
      lambdaExpressions.UsingForeachCapturedVariable();
      Console.ReadKey();
    }

    public void LambdaCreation()
    {
      Func<int, int> squareRoot = x => x * x;
      //Func<int, int> squareRoot = x => { return x * x; }; //equvivalent

      int result = squareRoot(3);
      Console.WriteLine("Using lambda for square root calculation for value 3 : " + result);

      //Explicitely defined lambda parameters
      Func<int, int, int> sum = (int a, int b) => a + b;

      //Lambda Expression
      Expression<Func<int, int>> squareRootExp = x => x * x;

      //Compile expression to method at runtime
      var method = squareRootExp.Compile();

      result = method(3);
      Console.WriteLine("Using lambda Exp. for square root calculation for value 3 : " + result);
    }

    public void VariableCapture()
    {
      int factor = 2;
      Func<int, int> multiplier = n => n * factor;
      factor = 4; // This value will be used in lambda
      int result = multiplier(3);
      Console.WriteLine();
      Console.WriteLine("Using captured varible inside lambda for multiplification of {0} by factor {1} = {2}", 3, factor, result);
    }

    public void UpdateOfLocalVaribale()
    {
      Console.WriteLine();
      Console.WriteLine("Updating local variable from lambda: ");
      int seed = 0;
      Func<int> natural = () => seed++;
      Console.WriteLine(natural());
      Console.WriteLine(natural()); 
      Console.WriteLine(seed); 
    }

    public void ForLoopVariableCapture()
    {
      Console.WriteLine();
      Console.WriteLine("Using for variable captured in lambda :");

      Action[] actions = new Action[3];
      for (int i = 0; i < 3; i++)
      {
        actions[i] = () => Console.WriteLine( + i);
      }
      
      foreach (Action a in actions)
      {
        a();
      }
    }

    public void ForLoopVariableCaptureWithLocalVar()
    {
      Console.WriteLine();
      Console.WriteLine("Using for variable copied to local and captured in lambda :");

      Action[] actions = new Action[3];
      for (int i = 0; i < 3; i++)
      {
        int localVar = i;
        actions[i] = () => Console.WriteLine(localVar);
      }

      foreach (Action a in actions)
      {
        a();
      }
    }

    public void UsingForeachCapturedVariable()
    {
      Console.WriteLine();
      Console.WriteLine("Using for variable copied to local and captured in lambda :");

      //Different behaviour between C# 4 (and previous) and 5
      Action[] actions = new Action[3]; 
      int i = 0;
      foreach (char c in "abc")
      {
        actions[i++] = () => Console.Write(c);
      }
	  
      foreach (Action a in actions)
      {
          a(); // C#5: abc, C#4: ccc ... "Breaking change!"
      }
    }
  }
}
