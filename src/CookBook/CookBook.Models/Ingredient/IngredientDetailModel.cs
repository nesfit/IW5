using CookBook.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public class IngredientDetailModel : IId
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}