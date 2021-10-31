using System;
using System.ComponentModel;
using System.Reflection;
using CookBook.Common.Attributes;

namespace CookBook.Web.App.Extensions
{
    public static class EnumExtensions
    {
        public static string GetReadableName(this Enum enumValue)
        {
            var enumField = enumValue.GetType().GetField(enumValue.ToString());

            var localizableDescriptionAttribute = enumField?.GetCustomAttribute<LocalizableDescriptionAttribute>();
            var localizableDescription = localizableDescriptionAttribute?.GetLocalizedDescription();
            if (!string.IsNullOrWhiteSpace(localizableDescription))
            {
                return localizableDescription;
            }


            var descriptionAttribute = enumField?.GetCustomAttribute<DescriptionAttribute>();
            var description = descriptionAttribute?.Description;
            if (!string.IsNullOrWhiteSpace(description))
            {
                return description;
            }

            return enumValue.ToString();
        }
    }
}
