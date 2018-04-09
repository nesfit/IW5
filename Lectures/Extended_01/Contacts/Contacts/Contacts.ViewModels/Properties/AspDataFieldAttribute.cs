using System;

namespace Contacts.ViewModels.Properties
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class AspDataFieldAttribute : Attribute { }
}