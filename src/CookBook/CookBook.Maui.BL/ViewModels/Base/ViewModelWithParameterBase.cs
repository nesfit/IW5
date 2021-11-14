namespace CookBook.Maui.BL.ViewModels;

public class ViewModelWithParameterBase<TDetailModel, TParameter> : ViewModelBase, IViewModelWithParameter<TDetailModel, TParameter>
{
    public TParameter? Parameter { get; set; }
    public TDetailModel Item { get; set; }
}
