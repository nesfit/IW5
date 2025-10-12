using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Web.App.Resources.Texts;
using CookBook.Web.BL.Api;
using CookBook.Web.BL.Facades;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CookBook.Web.App.Pages
{
    public partial class RecipeEditPage
    {
        [Inject]
        private NavigationManager navigationManager { get; set; } = null!;

        [Inject]
        private RecipeFacade RecipeFacade { get; set; } = null!;
        [Inject]
        private IngredientFacade IngredientFacade { get; set; } = null!;

        private RecipeDetailModel _data = GetNewRecipeDetailModel();
        private RecipeDetailModel Data 
        { 
            get => _data;
            set
            {
                _data = value;
                if (editContext?.Model != _data)
                {
                    editContext = new EditContext(_data);
                    validationMessageStore = new ValidationMessageStore(editContext);
                }
            }
        }

        [Parameter]
        public Guid Id { get; init; }

        private ICollection<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

        private RecipeDetailIngredientModel NewIngredientModel { get; set; } = GetNewRecipeDetailIngredientModel();

        private EditContext? editContext;
        private ValidationMessageStore? validationMessageStore;

        public List<string> GeneralErrorMessages { get; set; } = new();

        private int DurationHours
        {
            get => Data.Duration.Hours;
            set => Data.Duration = new TimeSpan(value, DurationMinutes, 0);
        }

        private int DurationMinutes
        {
            get => Data.Duration.Minutes;
            set => Data.Duration = new TimeSpan(DurationHours, value, 0);
        }

        private string SelectedIngredientName
        {
            get
            {
                return NewIngredientModel.Ingredient.Name;
            }
            set
            {
                NewIngredientModel.Ingredient = Ingredients.First(t => t.Name == value);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                Data = await RecipeFacade.GetByIdAsync(Id);
            }

            Ingredients = await IngredientFacade.GetAllAsync();

            editContext = new EditContext(Data);
            validationMessageStore = new ValidationMessageStore(editContext);

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            GeneralErrorMessages.Clear();
            
            // Clear any previous API validation errors
            validationMessageStore?.Clear();

            if (editContext == null)
            {
                return;
            }

            if (!editContext.Validate())
            {
                // Client-side validation failed
                return;
            }

            try
            {
                await RecipeFacade.SaveAsync(Data);
                navigationManager.NavigateTo($"/recipes");
            }
            catch (ApiException<HttpValidationProblemDetails> ex)
            {
                HandleValidationErrors(ex.Result);
            }
            catch (ApiException ex) when (ex.StatusCode == 400)
            {
                HandleGenericValidationError(ex);
            }
            catch (Exception ex)
            {
                AddGeneralError(string.Format(RecipeEditPageResources.ErrorMessage_General, ex.Message));
            }
        }

        public async Task Delete()
        {
            GeneralErrorMessages.Clear();

            try
            {
                await RecipeFacade.DeleteAsync(Id);
                navigationManager.NavigateTo($"/recipes");
            }
            catch (Exception ex)
            {
                AddGeneralError(string.Format(RecipeEditPageResources.ErrorMessage_FailedToDelete, ex.Message));
            }
        }

        public void DeleteIngredient(RecipeDetailIngredientModel ingredient)
        {
            var ingredientIndex = Data.IngredientAmounts.IndexOf(ingredient);
            Data.IngredientAmounts.RemoveAt(ingredientIndex);
        }

        public void AddIngredient()
        {
            Data.IngredientAmounts.Add(NewIngredientModel);
            NewIngredientModel = GetNewRecipeDetailIngredientModel();
        }

        private static RecipeDetailModel GetNewRecipeDetailModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                Duration = TimeSpan.Zero,
                FoodType = FoodType.Unknown,
            };

        private static RecipeDetailIngredientModel GetNewRecipeDetailIngredientModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Amount = 0,
                Unit = Unit.Unknown,
                Ingredient = new IngredientListModel
                {
                    Id = Guid.Empty,
                    Name = string.Empty
                }
            };

        private void AddGeneralError(string message)
        {
            GeneralErrorMessages.Add(message);
            StateHasChanged();
        }

        private void HandleValidationErrors(HttpValidationProblemDetails validationProblem)
        {
            if (editContext == null || validationMessageStore == null || validationProblem?.Errors == null) return;

            validationMessageStore.Clear();

            foreach (var error in validationProblem.Errors)
            {
                // Handle general errors
                if (string.IsNullOrEmpty(error.Key) || error.Key == "")
                {
                    foreach (var message in error.Value)
                    {
                        AddGeneralError(message);
                    }
                }
                else
                {
                    // Handle field-specific errors
                    var fieldIdentifier = new FieldIdentifier(Data, error.Key);
                    foreach (var message in error.Value)
                    {
                        validationMessageStore.Add(fieldIdentifier, message);
                    }
                }
            }

            editContext.NotifyValidationStateChanged();
            StateHasChanged();
        }

        private void HandleGenericValidationError(ApiException ex)
        {
            if (editContext == null)
            {
                AddGeneralError(RecipeEditPageResources.ErrorMessage_General);
                return;
            }

            // Try to parswe validation errors from the response content
            try
            {
                var response = ex.Response;
                if (!string.IsNullOrEmpty(response))
                {
                    var validationProblem = System.Text.Json.JsonSerializer.Deserialize<HttpValidationProblemDetails>(response, new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (validationProblem?.Errors != null)
                    {
                        HandleValidationErrors(validationProblem);
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }

            AddGeneralError(RecipeEditPageResources.ErrorMessage_General);
        }
    }
}
