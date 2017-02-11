using System.Data.Entity;
using EFSimpleExample.Models;

namespace EFSimpleExample
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("EFSimpleExample") {}
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}