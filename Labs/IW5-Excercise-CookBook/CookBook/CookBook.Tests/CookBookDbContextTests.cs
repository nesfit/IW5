using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.DAL;
using CookBook.DAL.Migrations;
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
