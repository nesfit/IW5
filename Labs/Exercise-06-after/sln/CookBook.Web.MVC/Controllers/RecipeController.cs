﻿using CookBook.BL.Common.Services;
using CookBook.BL.Web.Api;
using CookBook.BL.Web.Facades;
using CookBook.Web.MVC.Models;
using CookBook.Web.MVC.ViewModels.Recipe;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace CookBook.Web.MVC.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeFacade _recipeFacade;
        private readonly IngredientFacade _ingredientFacade;
        private readonly ISerializerService serializerService;

        public RecipeController(
            RecipeFacade recipeFacade,
            IngredientFacade ingredientFacade,
            ISerializerService serializerService)
        {
            _recipeFacade = recipeFacade;
            _ingredientFacade = ingredientFacade;
            this.serializerService = serializerService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var recipes = await _recipeFacade.GetAllAsync();
            var recipeListViewModel = new RecipeListViewModel
            {
                Recipes = recipes
            };
            return View(recipeListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var recipe = await _recipeFacade.GetByIdAsync(id);
            var recipeDetailViewModel = new RecipeDetailViewModel
            {
                RecipeDetail = recipe
            };
            return View(recipeDetailViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> New(RecipeNewViewModel recipeNewViewModel)
        {
            recipeNewViewModel.IngredientsAll ??= await _ingredientFacade.GetAllAsync();
            recipeNewViewModel.RecipeNewModel ??= new RecipeNewModel
            {
                Ingredients = new List<RecipeNewIngredientModel>()
            };

            return View(recipeNewViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(RecipeNewViewModel recipeNewViewModel)
        {
            var currentCulture = CultureInfo.CurrentCulture;

            if (TimeSpan.TryParse(recipeNewViewModel.DurationText, out TimeSpan duration))
            {
                recipeNewViewModel.RecipeNewModel.Duration = duration;
            }

            try
            {
                await _recipeFacade.InsertAsync(recipeNewViewModel.RecipeNewModel, culture: currentCulture.Name);
            }
            catch (ApiException e)
            {
                var validationErrorsModel = serializerService.Deserialize<ValidationErrorsModel<RecipeNewValidationErrorsModel>>(e.Response);

                foreach (var nameError in validationErrorsModel.Errors.Name)
                {
                    ModelState.AddModelError("RecipeNewModel.Name", nameError);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(nameof(New));
            }

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(RecipeNewViewModel recipeNewViewModel)
        {
            recipeNewViewModel.RecipeNewModel.Ingredients ??= new List<RecipeNewIngredientModel>();
            recipeNewViewModel.RecipeNewModel.Ingredients.Add(new RecipeNewIngredientModel());
            return View(nameof(New), recipeNewViewModel);
        }
    }
}