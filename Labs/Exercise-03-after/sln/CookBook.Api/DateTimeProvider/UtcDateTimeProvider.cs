using System;

namespace CookBook.Api.DateTimeProvider
{
    public class UtcDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
