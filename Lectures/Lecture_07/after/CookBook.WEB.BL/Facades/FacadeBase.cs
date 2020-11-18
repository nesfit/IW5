using CookBook.BL.Common.Facades;

namespace CookBook.WEB.BL.Facades
{
    public class FacadeBase : IAppFacade
    {
        protected virtual string apiVersion => "3";
        protected virtual string culture => "en";
    }
}