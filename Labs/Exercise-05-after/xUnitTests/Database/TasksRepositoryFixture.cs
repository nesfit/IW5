using IW5_Tests;

namespace xUnitTests.Database
{
    public class TasksRepositoryFixture : DatabaseFixture
    {
        public readonly TasksRepository Repository;

        public TasksRepositoryFixture()
        {
            Repository = new TasksRepository(() => CreateDbContext());
        }
    }
}