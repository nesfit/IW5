using System;
using CookBook.Common.Enums;
using CookBook.Common.Extensions;

namespace CookBook.App.Models
{
    public class EnumItem<T> where T : Enum
    {
        public EnumItem(T value)
        {
            Value = value;
            Name = value.GetReadableName();
        }
        
        public EnumItem()
        {
        }

        public string Name { get; set; }

        public T Value { get; set; }
    }
}
