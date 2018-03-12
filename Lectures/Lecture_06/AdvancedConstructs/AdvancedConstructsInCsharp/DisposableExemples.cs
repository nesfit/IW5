using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedConstructsInCsharp
{
    [TestClass]
    public class DisposableExemples
    {

        public void UsingWrapsDisposable()
        {
            using (var toDispose = new DisposableClass())
            {
                toDispose.Run();
            }
        }
    }

    //https://msdn.microsoft.com/en-us/library/system.idisposable(v=vs.110).aspx
    class DisposableClass : IDisposable
    {
        public void Run()
        {
        }

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableClass()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
            }

            // Free any unmanaged objects here.
            disposed = true;
        }
    }
}
