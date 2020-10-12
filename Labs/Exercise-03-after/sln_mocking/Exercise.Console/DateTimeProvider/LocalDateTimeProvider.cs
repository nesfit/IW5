using System;

namespace Exercise.Console.DateTimeProvider
{
    public class LocalDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
        public DateTime Today => DateTime.Today;
    }
}
