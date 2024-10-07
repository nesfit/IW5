using System.Resources;
using CookBook.Common.Attributes;
using CookBook.Common.Resources;

namespace CookBook.Common.Enums
{
    public enum FoodType
    {
        [FoodTypeDescription(nameof(FoodTypeResources.UnknownDescription))]
        Unknown = 0,

        [FoodTypeDescription(nameof(FoodTypeResources.MainDishDescription))]
        MainDish = 1,

        [FoodTypeDescription(nameof(FoodTypeResources.SoupDescription))]
        Soup = 2,

        [FoodTypeDescription(nameof(FoodTypeResources.DessertDescription))]
        Dessert = 3
    }


    public class FoodTypeDescription : LocalizableDescriptionAttribute
    {
        public FoodTypeDescription(string resourceName) : base(resourceName)
        {
        }

        protected override ResourceManager GetResource()
        {
            return FoodTypeResources.ResourceManager;
        }
    }
}
