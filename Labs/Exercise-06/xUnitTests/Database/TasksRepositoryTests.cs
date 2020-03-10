using System;
using System.Linq;
using IW5_Tests.Database;
using Xunit;

namespace xUnitTests.Database
{
    public class TasksRepositoryTests : IDisposable
    {
        private readonly TasksRepositoryFixture tasksRepositoryFixture;

        public TasksRepositoryTests()
        {
            this.tasksRepositoryFixture = new TasksRepositoryFixture();
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("")]
        [InlineData(null)]
        public void AddTest(string itemText)
        {
            //ARRANGE
            var repository = tasksRepositoryFixture.Repository;

            //ACT
            repository.Add(itemText);

            //ASSERT
            UserTask itemFromDb;
            using (var dbContext = tasksRepositoryFixture.CreateDbContext())
            {
                itemFromDb = dbContext.UserTasks.SingleOrDefault(t => t.Name == itemText);
            }

            Assert.NotNull(itemFromDb);
        }

        [Fact]
        public void GetTasksNoItemsTest()
        {
            //ARRANGE
            var repository = tasksRepositoryFixture.Repository;

            //ACT
            var tasks = repository.GetTasks();

            //ASSERT
            Assert.Empty(tasks);
        }
        [Fact]
        public void GetTasksSingleItemTest()
        {
            //ARRANGE
            var dbContext = tasksRepositoryFixture.CreateDbContext();
            dbContext.UserTasks.Add(new UserTask() { Name = "TEST" });
            dbContext.SaveChanges();

            var repository = tasksRepositoryFixture.Repository;

            //ACT
            var tasks = repository.GetTasks();

            //ASSERT
            Assert.Single(tasks);
        }

        [Fact]
        public void GetTasksMultipleItemsTest()
        {
            //ARRANGE
            var dbContext = tasksRepositoryFixture.CreateDbContext();
            var expectedTaskCount = 2;

            for (int i = 0; i < expectedTaskCount; i++)
            {
                dbContext.UserTasks.Add(new UserTask() { Name = "TEST" });
            }
            dbContext.SaveChanges();

            var repository = tasksRepositoryFixture.Repository;

            //ACT
            var tasks = repository.GetTasks();

            //ASSERT
            var actualTaskCount = tasks.Count();
            Assert.True(expectedTaskCount == actualTaskCount, $"Expected {expectedTaskCount} tasks, got {actualTaskCount}");
        }


        public void Dispose()
        {
            tasksRepositoryFixture.Dispose();
        }
    }
}