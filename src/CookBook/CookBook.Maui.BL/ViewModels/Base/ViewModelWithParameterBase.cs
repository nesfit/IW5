namespace CookBook.Maui.BL.ViewModels;

public class ViewModelWithParameterBase<TParameter> : ViewModelBase
{
    public TParameter? Parameter { get; set; }
}
