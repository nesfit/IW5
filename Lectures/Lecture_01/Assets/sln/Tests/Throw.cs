using System;
using Xunit;

namespace Tests
{
    public class Throw
    {
        private readonly int[] numbers = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };

        public int GetNumber(int index)
        {
            if (index < 0 || index >= numbers.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return numbers[index];
        }


        [Fact]
        public void Test()
        {
            Assert.Equal(6, GetNumber(2));
            Assert.Throws<IndexOutOfRangeException>(() => GetNumber(15));
        }
    }
}