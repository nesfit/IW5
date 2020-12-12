using System;

namespace CookBook.BL.Mobile.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase<Guid>
    {
        public RecipeDetailViewModel(Guid viewModelParameter)
            : base(viewModelParameter)
        {
        }
    }
}