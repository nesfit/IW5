using System.Linq;
using IW5_03_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DatabaseStoreTests
    {
        // the only think you need to do to fix this test is to update the connection string in app.config
        // see also www.connectionstrings.com
        [TestMethod]
        public void ValidName_Add_AddsToDatabase()
        {
            // GIVEN
            const string ExpectedName = "ExpectedName";
            var store = new DatabaseStore();

            // WHEN
            store.Add(ExpectedName);

            // THEN
            var contains = store.GetTasks().Any(t => t.Name == ExpectedName);
            Assert.IsTrue(contains, "The added item should be available to others in the database.");
        }
    }
}
