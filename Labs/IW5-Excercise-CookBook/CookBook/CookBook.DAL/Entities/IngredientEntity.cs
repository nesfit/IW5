using CookBook.DAL.Entities.Base.Implementation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace CookBook.DAL.Entities
{
    public class IngredientEntity : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Int32 Argb
        {
            get
            {
                return Color.ToArgb();
            }
            set
            {
                Color = Color.FromArgb(value);
            }
        }
        [NotMapped]
        public Color Color { get; set; }
    }
}
