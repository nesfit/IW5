using System;

namespace Contacts.ViewModels.Properties
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AspChildControlTypeAttribute : Attribute
    {
        public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
        {
            TagName = tagName;
            ControlType = controlType;
        }

        [NotNull] public string TagName { get; private set; }
        [NotNull] public Type ControlType { get; private set; }
    }
}