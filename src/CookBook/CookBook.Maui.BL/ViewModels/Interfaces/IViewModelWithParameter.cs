namespace CookBook.Maui.BL.ViewModels;

public interface IViewModelWithParameter<TDetailModel, TParameter> : IViewModel
{
    TParameter? Parameter { get; set; }
    TDetailModel Item { get; set; }
}
