using DependencyInjection.Loggers;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection.Services
{
    public class AdditionService : IService
    {
        private readonly ILogger logger;

        public AdditionService()
        {
            logger = new ConsoleLogger();
        }

        public AdditionService(ILogger logger)
        {
            this.logger = logger;
        }

        public int Add(IList<int> numbers)
        {
            var result = numbers.Sum();
            logger.Log($"Addition result: {result}");
            return result;
        }
    }
}