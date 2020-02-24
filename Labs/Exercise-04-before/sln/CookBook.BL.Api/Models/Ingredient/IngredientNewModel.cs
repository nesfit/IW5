using AutoMapper;
using CookBook.Common.Extensions;
using CookBook.Common.Resources;
using CookBook.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace CookBook.BL.Api.Models.Ingredient
{
    public class IngredientNewModel
    {
        [Required(ErrorMessageResourceType = typeof(IngredientNewModelResources), ErrorMessageResourceName = nameof(IngredientNewModelResources.Name_Required))]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceType = typeof(IngredientNewModelResources), ErrorMessageResourceName = nameof(IngredientNewModelResources.Name_StringLength))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(IngredientNewModelResources), ErrorMessageResourceName = nameof(IngredientNewModelResources.Description_Required))]
        [MinLength(10, ErrorMessageResourceType = typeof(IngredientNewModelResources), ErrorMessageResourceName = nameof(IngredientNewModelResources.Description_MinLength))]
        public string Description { get; set; }
    }

    public class IngredientNewModelMapperProfile : Profile
    {
        public IngredientNewModelMapperProfile()
        {
            CreateMap<IngredientNewModel, IngredientEntity>()
                .Ignore(dst => dst.Id);
        }
    }
}