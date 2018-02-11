using System;
using Xunit;

namespace OOP.Generics
{
    public class GenericStackSample
    {
        [Fact]
        public void StackTest()
        {
            var strack = new Stack<string>();
            var item = "hello";
            strack.Push(item);
            string hello = strack.Pop();

            Assert.Equal(item, hello);
            Assert.Same(item, hello);
        }

        [Fact]
        public void ObjectStackNoBoxingTest()
        {
            var strack = new Stack<int>();
            var item = 1;
            strack.Push(item);
            int one = strack.Pop();

            Assert.Equal(item, one);
        }
    }
}