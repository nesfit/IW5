using System;

namespace AdvancedConstructsInCsharp
{
  public class DelegateExamples
  {
    public static void RunExamples()
    {
      var delegateExamples = new DelegateExamples();
      Console.WriteLine("\r\n\r\n--- Delegate Examples ---");

      //Example 1 - Delegate creation and invokation
      delegateExamples.BasicExample();
      Console.ReadKey();

      //Example 2 - Multicast capabilities of delegate
      delegateExamples.MutliCastExample();
      Console.ReadKey();

      //Example 3 - Management of subscribers
      delegateExamples.SubscriberManagement();
      Console.ReadKey();

      //Example 4 - Generic delegates
      delegateExamples.GenericDelegates();
      Console.ReadKey();
    }

    #region Basic Example
    private delegate int Transformer(int x); //Delegate variable

    private static int Square(int x) // private static method
    {
      return x * x;
    }

    public void BasicExample()
    {
      Transformer t = Square; // Creates delegate instance
      //Transformer t = new Transformer(Square); // Equivalent 

      int result = t(3);  // Invokes the method using the reference stored in variable    
      //result = t.Invoke(3) Equivalent

      Console.WriteLine(result); // 9
    }
    #endregion

    #region Multicast example
    private delegate void HelloWorld();
    private void Hello()
    {
      Console.WriteLine("Hello");
    }

    private void World()
    {
      Console.WriteLine("world!");
    }

    public void MutliCastExample()
    {
      HelloWorld helloWorld = Hello;
      helloWorld += World;
      helloWorld();
    }
    #endregion

    #region Advanced operatations
    public void SubscriberManagement()
    {
      HelloWorld delegateVar = null;
      delegateVar += Hello;
      delegateVar += World;
      delegateVar -= Hello; //Removing target
      var numOfTargets = delegateVar.GetInvocationList().Length; //Getting num of targets
      Console.WriteLine("Delegate has {0} targets", numOfTargets);

      delegateVar = null; //Cleaning the delegate
      if (delegateVar == null) //Checking if there is any target
      {
        Console.WriteLine("Delegate is empty!");
      }

    }
    #endregion

    #region Generic delegates
    public delegate T Transformer<T>(T arg); //Delegate with type parameter

    public void GenericDelegates()
    {
      Transformer<int> a = Square; //Using custom delegate with generic param 
      Func<int, int> b = Square; // Using predefined generic delegate  

      Console.WriteLine("Invoking custom delegate {0}", a(3));
      Console.WriteLine("Invoking Func delegate {0}", b(3));
    }
    #endregion

  }
}
