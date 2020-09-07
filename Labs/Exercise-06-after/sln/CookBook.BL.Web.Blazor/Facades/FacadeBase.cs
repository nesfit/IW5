using CookBook.BL.Common.Facades;

namespace CookBook.BL.Web.Blazor.Facades
{
    public class FacadeBase : IAppFacade
    {
        protected virtual string apiVersion => "3";
    }
}