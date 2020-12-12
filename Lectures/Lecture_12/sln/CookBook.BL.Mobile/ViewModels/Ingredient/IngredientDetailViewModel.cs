using System;

namespace CookBook.BL.Mobile.ViewModels
{
    public class IngredientDetailViewModel : ViewModelBase<Guid>
    {
        public IngredientDetailViewModel(Guid viewModelParameter)
            : base(viewModelParameter)
        {
        }
    }
}