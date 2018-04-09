using System;

namespace Contacts.MVVM.Framework.Properties
{
    /// <summary>
    /// Implicitly apply [NotNull]/[ItemNotNull] annotation to all the of type members and parameters
    /// in particular scope where this annotation is used (type declaration or whole assembly).
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Assembly)]
    public sealed class ImplicitNotNullAttribute : Attribute { }
}