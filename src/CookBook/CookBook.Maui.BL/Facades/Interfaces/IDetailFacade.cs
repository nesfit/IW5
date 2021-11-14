namespace CookBook.Maui.BL.Facades;

public interface IDetailFacade<TDetailModel>
{
    Task<TDetailModel> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task UpsertAsync(TDetailModel model);
}
