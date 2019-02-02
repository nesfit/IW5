using System;
using System.Threading;
using Xunit;

namespace Tests
{
    public class Lock
    {
        private readonly object thisLock = new object();

        private int increment;
        private void MethodWithLock()
        {
            lock (thisLock)
            {
                Thread.Sleep(10);
                increment++;
            }
        }

        [Fact]
        private void Test()
        {
            const int attempts = 10;
            for (var i = 0; i < attempts; i++)
            {
                new Thread(MethodWithLock).Start();
            }

            Assert.True(increment < attempts);
        }
    }
}