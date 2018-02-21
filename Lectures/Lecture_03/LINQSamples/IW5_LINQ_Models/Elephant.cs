using System;

namespace IW5_LINQ_Models
{
    public class Elephant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TrunkLength { get; set; }
        public DateTime Birth { get; set; } = DateTime.Now;
        public TimeSpan Age => DateTime.Now - Birth;
    }
}
