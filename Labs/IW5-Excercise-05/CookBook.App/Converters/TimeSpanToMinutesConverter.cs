using System;
using System.Globalization;
using System.Windows.Data;

namespace CookBook.App.Converters
{
    public class TimeSpanToMinutesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = value as TimeSpan?;

            return time?.TotalMinutes;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var minutes = value?.ToString();

            if (minutes != null)
            {
                int minutesCount = 0;
                if (int.TryParse(minutes, out minutesCount))
                {
                    return TimeSpan.FromMinutes(minutesCount);
                }
            }

            return null;
        }
    }
}