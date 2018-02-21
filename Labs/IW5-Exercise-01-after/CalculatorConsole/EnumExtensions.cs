using System;
using System.ComponentModel;
using System.Linq;

namespace CalculatorConsole
{
    internal static class EnumExtensions
    {
        internal static string GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Any())
                return attributes[0].Description;
            return value.ToString();
        }
    }
}