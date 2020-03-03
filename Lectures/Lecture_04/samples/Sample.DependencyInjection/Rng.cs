using System;

namespace Sample.DependencyInjection
{
    public class Rng
    {
        private readonly ILogger logger;
        private readonly Random random = new Random();

        public Rng(ILogger logger)
        {
            this.logger = logger;
        }

        public void Generate(int count)
        {
            for (var i = 0; i < count; ++i)
            {
                logger.Log($"{random.Next()}");
            }
        }
    }
}
