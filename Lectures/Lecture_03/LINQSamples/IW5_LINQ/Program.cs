using IW5_LINQ_Models;
using System.Collections.Generic;
using System.Linq;

namespace IW5_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "Jay", "May", "Hugo", "Filip", "Anna" };
            string[] surnames = { "Doe", "Klzky", "Maly" };

            // LINQ - Fluent syntax
            // sequence in -> sequence out
            List<string> namesFiltered1 = names
                .Where(name => name.Contains("a"))
                .OrderBy(x => x.Length)
                .Select(x => x.ToUpper()).ToList();

            // LINQ - Query syntax
            // sequence in -> sequence out
            List<string> namesFiltered2 =
                (from name in names
                 where name.Contains("a")
                 orderby name.Length
                 select name.ToUpper()).ToList();

            // joins two collections - result is collection of names with surnames
            IEnumerable<string> namesWithSurnames = names
                .Zip(surnames, (name, surname) => name + " " + surname).ToList();

            // LINQ sequence in -> scalar value out (integer)
            int[] numbers = { 1, 2, 3, 4, 5 };
            int minimumValue = numbers.Min();
            int maximalValue = numbers.Max();
            int numbersSum = numbers.Sum();

            // LINQ sequence in -> scalar value out (boolean)
            bool areAllNumbersPositive = numbers.All(x => x >= 0);
            bool isAnyNumberGreaterThanFive = numbers.Any(x => x > 5);

            // working with collection of objects (Elephant)
            var elephants = new List<Elephant>()
            {
                new Elephant { Name = "Elephant1", TrunkLength = 6},
                new Elephant { Name = "Elephant2", TrunkLength = 7 },
                new Elephant { Name = "Elephant3", TrunkLength = 6}
            };

            var elephantsGroupedByTrunkLength = elephants
                .GroupBy(x => x.TrunkLength)
                .Select(x => new
                {
                    GroupTrunkLength = x.Key,
                    Elephants = x.ToList(),
                    TrunkLengthSeum = x.ToList().Sum(y => y.TrunkLength)
                }).ToList();

            // load or save elephants from/to DB - DB has to exist and valid connection string has to be set in PetsDbContext
            //var elephantService = new ElephantService();
            //elephantService.SaveElephants(elephants);
            //var elephantsLoadedFromDB = elephantService.GetAllElephants();
        }
    }
}
