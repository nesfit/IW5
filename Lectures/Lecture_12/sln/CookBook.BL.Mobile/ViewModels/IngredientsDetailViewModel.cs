using System;

namespace CookBook.BL.Mobile.ViewModels
{
    public class IngredientsDetailViewModel : ViewModelBase<Guid>
    {
        public IngredientsDetailViewModel(Guid viewModelParameter)
            : base(viewModelParameter)
        {
        }
    }
}