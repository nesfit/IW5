using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedConstructsInCsharp
{
    [TestClass]
    public class DisposableExemples
    {
        [TestMethod]
        public void UsingWrapsDisposable()
        {
            // only IDisposable can be used in using block
            // this is expanded by compiler to try-finally
            using (var toDispose = new DisposableClass())
            {
                toDispose.Run();
            }          
        }
    }

    //https://msdn.microsoft.com/en-us/library/system.idisposable(v=vs.110).aspx
    class DisposableClass : IDisposable
    {
        // if you need to release the resource see, if it implements IDisposable
        FileStream f;
        public void Run()
        {
        }

        // Flag: Has Dispose already been called?
        private bool disposed = false;

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
                // e.g. f.Dispose();
            }

            // Free any unmanaged objects here.
            disposed = true;
        }
    }
}
