using System;

namespace Contacts.MVVM.Framework.Properties
{
    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC view component name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcViewComponentAttribute : Attribute { }
}