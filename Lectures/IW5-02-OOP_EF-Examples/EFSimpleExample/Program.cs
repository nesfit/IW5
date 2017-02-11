using System;
using System.Linq;

using EFSimpleExample.Models;

namespace EFSimpleExample
{
    /// <summary>
    ///     Based on https://msdn.microsoft.com/en-us/data/jj193542.aspx
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create and save a new Blog 
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog {Name = name};
                db.Blogs.Add(blog);
                db.SaveChanges();

                // Display all Blogs from the database 
                var query = from b in db.Blogs
                    orderby b.Name
                    select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                    Console.WriteLine(item.Name);

                // Create and save a new Post 
                Console.Write($"Enter a title of a new Post assigned to the {blog.Name} blog: ");
                var title = Console.ReadLine();
                Console.Write($"Enter a content of a {title} post: ");
                var content = Console.ReadLine();

                var post = new Post {Title = title, Content = content, Blog = blog};

                db.Posts.Add(post);
                db.SaveChanges();

                Console.WriteLine($"All posts for the {blog.Name} blog:");
                foreach (var postItem in blog.Posts)
                {
                    Console.WriteLine($"{postItem.Title} - {postItem.Content}");
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}