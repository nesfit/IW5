using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IW5_03_Tests.Database;

namespace IW5_03_Tests
{
    internal class DatabaseStore
    {
        public void Add(string name)
        {
            using (var db = new TasksDbContext())
            {
                string connectionString = db.Database.Connection.ConnectionString;
                Debug.WriteLine("Used connection string: '{0}'", connectionString);

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
            using (var db = new TasksDbContext())
            {
                return db.UserTasks.ToList();
            }
        }
    }
}
