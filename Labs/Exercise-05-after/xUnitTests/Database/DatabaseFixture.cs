using System;
using IW5_Tests.Database;
using Microsoft.EntityFrameworkCore;

namespace xUnitTests.Database
{
    public class DatabaseFixture
    {
        protected string DbName = Guid.NewGuid().ToString();
        public DatabaseFixture()
        {
            PrepareDatabase();
        }

        public TasksDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TasksDbContext>();

            //optionsBuilder.UseSqlServer($@"Server=.\SQLEXPRESS;Database=TasksTests;Persist Security Info=True;Integrated Security=True");

            optionsBuilder.UseInMemoryDatabase(DbName);
            optionsBuilder.EnableSensitiveDataLogging();

            return new TasksDbContext(optionsBuilder.Options);
        }
        public void PrepareDatabase()
        {
            using (var dbx = CreateDbContext())
            {
                dbx.Database.EnsureCreated();
            }
        }

        public void TearDownDatabase()
        {
            using (var dbx = CreateDbContext())
            {
                dbx.Database.EnsureDeleted();
            }
        }

        public void Dispose()
        {
            TearDownDatabase();
        }
    }
}