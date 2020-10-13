using System;
using System.Threading;

namespace UITests.Helpers
{
    internal static class DemoHelper
    {
        public static void Pause(int secondsToPause = 1)
        {
            Thread.Sleep(TimeSpan.FromSeconds(secondsToPause));
        }
    }
}
