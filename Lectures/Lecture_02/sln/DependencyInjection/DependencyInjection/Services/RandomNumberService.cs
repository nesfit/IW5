using DependencyInjection.Loggers;
using System;
using System.Collections.Generic;

namespace DependencyInjection.Services
{
    public class RandomNumberService : IService
    {
        private readonly ILogger logger;
        private readonly Random random = new Random();

        public RandomNumberService()
        {
            logger = new ConsoleLogger();
        }

        public RandomNumberService(ILogger logger)
        {
            this.logger = logger;
        }

        public IList<int> Generate(int count)
        {
            var generatedNumbers = new List<int>();
            for (var i = 0; i < count; i++)
            {
                var generatedNumber = random.Next(1, 10);
                generatedNumbers.Add(generatedNumber);
                logger.Log(($"Generated number: {generatedNumber}"));
            }
            return generatedNumbers;
        }
    }
}