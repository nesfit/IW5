using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace IW5_Tests.Database
{
    public class TasksDbContext : DbContext
    {
        public DbSet<UserTask> UserTasks { get; set; }
        public TasksDbContext(DbContextOptions<TasksDbContext> contextOptions)
            : base(contextOptions)
        {
        }
    }
}
