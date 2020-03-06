using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace IW5_Tests.Database
{
    internal class TasksDbContext : DbContext
    {
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("TasksDbContext");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
