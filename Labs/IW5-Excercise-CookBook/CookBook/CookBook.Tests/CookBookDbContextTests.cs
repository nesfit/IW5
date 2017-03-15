using System;
using System.Linq;
using CookBook.BL;
using CookBook.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CookBook.Tests
{
    [TestClass]
    public class CookBookDbContextTests
    {
        [TestMethod]
        public void DbConnectionTest()
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                cookBookDbContext.Recipes.Any();
            }
        }
    }
}
