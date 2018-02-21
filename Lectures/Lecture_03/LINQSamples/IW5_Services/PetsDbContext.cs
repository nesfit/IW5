using IW5_LINQ_Models;
using System.Data.Entity;

namespace IW5_Services
{
    public class PetsDbContext : DbContext
    {
        public DbSet<Elephant> Elephants { get; set; }

        // add connection string to an existing DB instead of "nameOrConnectionString"
        public PetsDbContext() : base("nameOrConnectionString")
        {
        }
    }
}
