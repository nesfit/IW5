using DependencyInjection.Loggers;
using System.Collections.Generic;

namespace DependencyInjection.Services
{
    public class MultiplicationService : IService
    {
        private readonly ILogger logger;

        public MultiplicationService()
        {
            logger = new ConsoleLogger();
        }

        public MultiplicationService(ILogger logger)
        {
            this.logger = logger;
        }

        public int Multiply(IList<int> numbers)
        {
            var result = 1;

            foreach (var number in numbers)
            {
                result *= number;
            }

            logger.Log($"Multiplication result: {result}");

            return result;
        }
    }
}