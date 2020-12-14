using CookBook.BL.Common.Facades;

namespace CookBook.BL.Mobile.Facades
{
    public class FacadeBase : IAppFacade
    {
        protected virtual string apiVersion => "3";
        protected virtual string culture => "en";
    }
}