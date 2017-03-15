using System;
using System.Linq;
using CookBook.BL;
using CookBook.DAL;
using Xunit;

namespace CookBook.Tests
{
    public class CookBookDbContextTests
    {
        [Fact]
        public void DbConnectionTest()
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                cookBookDbContext.Recipes.Any();
            }
        }
    }
}
