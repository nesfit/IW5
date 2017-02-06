using System.Data.Entity;

namespace IW5_03_Tests.Database
{
    internal class TasksDbContext : DbContext
    {
        public DbSet<UserTask> UserTasks { get; set; }
    }
}
