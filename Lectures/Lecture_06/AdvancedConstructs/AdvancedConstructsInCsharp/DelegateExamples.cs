using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedConstructsInCsharp
{
    [TestClass]
    public class DelegateExamples
    {
        #region Basic Example
        
        private delegate int Transformer(int x); //Delegate variable

        private static int Square(int x) // private static method
        {
            return x * x;
        }

        [TestMethod]
        public void Delegate_creation_and_invokation()
        {
            Transformer t = Square; // Creates delegate instance
            //Transformer t = new Transformer(Square); // Equivalent 

            int result = t(3);  // Invokes the method using the reference stored in variable    
            //result = t.Invoke(3) Equivalent

            Assert.AreEqual(9, result);
            Console.WriteLine(result);
        }

        #endregion

        #region Multicast example
        
        private delegate string HelloWorld();
        
        private string Hello()
        {
            return "Hello";
        }

        private string World()
        {
            return "world!";
        }

        [TestMethod]
        public void MutliCastExample()
        {
            HelloWorld helloWorld = Hello;
            helloWorld += World;
            string result = helloWorld(); // both methods executed, but last result returned
            Assert.AreEqual("world!", result);
        }

        #endregion

        #region Advanced operatations
        
        [TestMethod]
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

            Assert.AreEqual(1, numOfTargets);
        }

        #endregion

        #region Generic delegates
        
        public delegate T Transformer<T>(T arg); //Delegate with type parameter

        [TestMethod]
        public void GenericDelegates()
        {
            Transformer<int> a = Square; //Using custom delegate with generic param 
            Func<int, int> b = Square; // Using predefined generic delegate  

            Assert.AreEqual(9, a(3));
            Assert.AreEqual(9, b(3));
        }

        #endregion

    }
}
