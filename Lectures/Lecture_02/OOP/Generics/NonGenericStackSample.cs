using System;
using OOP.Animals;
using Xunit;

namespace OOP.Generics
{
    public class NonGenericStackSample
    {
        [Fact]
        public void ObjectStackTest()
        {
            var objectStack = new ObjectStack();
            var item = "hello";
            objectStack.Push(item);
            var hello = objectStack.Pop();

            Assert.Equal(item,hello);
            Assert.Same(item,hello);
        }

        [Fact]
        public void ObjectStackBoxingTest()
        {
            var objectStack = new ObjectStack();
            var item = 1;
            objectStack.Push(item); //boxing
            int one = (int) objectStack.Pop(); //unboxing

            Assert.Equal(item, one);
        }

        [Fact]
        public void ObjectStackTypeNotSafeTest()
        {
            var objectStack = new ObjectStack();
            var item = "hello";
            objectStack.Push(item);

            Assert.Throws<InvalidCastException>(() => (int) objectStack.Pop());
        }
    }
}