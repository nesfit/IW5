namespace CookBook.Maui.BL.Facades;

public interface IListFacade<TListModel>
{
    Task<List<TListModel>> GetAllAsync();
}
