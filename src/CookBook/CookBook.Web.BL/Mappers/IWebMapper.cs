namespace CookBook.Web.BL.Mappers
{
    public interface IWebMapper<in TDetailModel, TListModel>
    {
        TListModel ToListModel(TDetailModel detailModel);
        IList<TListModel> ToListModels(IEnumerable<TDetailModel> detailModels);
    }
}