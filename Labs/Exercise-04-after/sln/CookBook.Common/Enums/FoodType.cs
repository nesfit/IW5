using System.ComponentModel;

namespace CookBook.Models
{
    public enum FoodType
    {
        [Description("Neznámý")]
        Unknown = 0,
        [Description("Hlavní chod")]
        MainDish = 1,
        [Description("Polévka")]
        Soup = 2,
        [Description("Dezert")]
        Dessert = 3
    }
}