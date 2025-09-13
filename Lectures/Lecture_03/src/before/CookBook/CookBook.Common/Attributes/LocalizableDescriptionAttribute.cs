using System;
using System.Resources;

namespace CookBook.Common.Attributes
{
    public abstract class LocalizableDescriptionAttribute : Attribute
    {
        private readonly string resourceName;

        public LocalizableDescriptionAttribute(string resourceName)
        {
            this.resourceName = resourceName;
        }

        protected abstract ResourceManager GetResource();

        public string? GetLocalizedDescription()
        {
            return GetResource()?.GetString(resourceName);
        }
    }
}
