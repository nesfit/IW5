using System;

namespace CookBook.Api.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
