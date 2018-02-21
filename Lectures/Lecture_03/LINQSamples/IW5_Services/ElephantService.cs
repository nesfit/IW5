using IW5_LINQ_Models;
using System.Collections.Generic;

namespace IW5_Services
{
    public class ElephantService
    {
        public IEnumerable<Elephant> GetAllElephants()
        {
            var elephants = new List<Elephant>();
            using (var petsDbContext = new PetsDbContext())
            {
                elephants.AddRange(petsDbContext.Elephants);
            }
            return elephants;
        }

        public void SaveElephants(IEnumerable<Elephant> elephants)
        {
            using (var petsDbContext = new PetsDbContext())
            {
                petsDbContext.Elephants.AddRange(elephants);
                petsDbContext.SaveChanges();
            }
        }
    }
}
