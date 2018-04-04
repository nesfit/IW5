namespace CookBook.BL.Models
{
    using System;

    using CookBook.DAL.Entities;

    public class RecipeListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FoodType Type { get; set; }
    }
}