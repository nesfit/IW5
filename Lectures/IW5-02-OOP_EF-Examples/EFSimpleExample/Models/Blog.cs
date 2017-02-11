using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace EFSimpleExample.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}