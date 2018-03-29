using CookBook.DAL.Entities;

namespace CookBook.BL.Models
{
    public class IngredientListModel
    {
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}