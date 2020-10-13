using Exercise.Console.DateTimeProvider;
using System;

namespace Exercise.Console.Tests.Mocks
{
    public class DateTimeProviderMock : IDateTimeProvider
    {
        public DateTime Now { get; set; }
        public DateTime Today => Now.Date;

        public DateTimeProviderMock(DateTime dateTime)
        {
            Now = dateTime;
        }
    }
}
