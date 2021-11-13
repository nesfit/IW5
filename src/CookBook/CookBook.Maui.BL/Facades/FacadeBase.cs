namespace CookBook.Maui.BL.Facades;

public abstract class FacadeBase<TDetailModel, TListModel> : IDetailFacade<TDetailModel>, IListFacade<TListModel>
{
    protected const string apiVersion = "3.0";
    protected const string culture = "en";

    public abstract Task<List<TListModel>> GetAllAsync();
    public abstract Task<TDetailModel> GetByIdAsync(Guid id);
    public abstract Task DeleteAsync(Guid id);
    public abstract Task UpsertAsync(TDetailModel ingredient);
}
