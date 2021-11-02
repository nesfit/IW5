using System.Globalization;
using CookBook.Common;
using CookBook.Common.BL.Facades;

namespace CookBook.Web.BL.Facades
{
    public abstract class FacadeBase<TDetailModel, TListModel> : IAppFacade
        where TDetailModel : IWithId
    {
        protected virtual string apiVersion => "3";
        protected virtual string culture => CultureInfo.DefaultThreadCurrentCulture?.Name ?? "cs";
    }
}
