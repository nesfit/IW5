using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IW5_Tests.Database;
using Microsoft.EntityFrameworkCore;

namespace IW5_Tests
{

    public class TasksRepository
    {
        private readonly Func<TasksDbContext> taskDbContextFactory;

        public TasksRepository(Func<TasksDbContext> taskDbContextFactory)
        {
            this.taskDbContextFactory = taskDbContextFactory;
        }
        public void Add(string name)
        {
            using (var db = taskDbContextFactory())
            {
                var toAdd = new UserTask()
                {
                    Name = name
                };

                db.UserTasks.Add(toAdd);
                db.SaveChanges();
            }
        }

        public IEnumerable<UserTask> GetTasks()
        {
            using (var db = taskDbContextFactory())
            {
                return db.UserTasks.ToList();
            }
        }
    }
}
