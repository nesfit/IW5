using System;
using System.ComponentModel;
using System.Reflection;

namespace CookBook.Web.App.Extensions
{
    public static class EnumExtensions
    {
        public static string GetReadableName(this Enum enumValue)
        {
            var enumField = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttribute = enumField?.GetCustomAttribute<DescriptionAttribute>();
            var description = descriptionAttribute?.Description;

            return !string.IsNullOrWhiteSpace(description) ? description : enumValue.ToString();
        }
    }
}
