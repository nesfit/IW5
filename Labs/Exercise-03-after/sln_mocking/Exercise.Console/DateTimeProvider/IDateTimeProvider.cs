using System;

namespace Exercise.Console.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}
