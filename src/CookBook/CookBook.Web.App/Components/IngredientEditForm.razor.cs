using System.Reflection.Metadata;
using CookBook.Common.Models;
using CookBook.Web.App.Resources.Texts;
using CookBook.Web.BL.Api;
using CookBook.Web.BL.Facades;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CookBook.Web.App
{
    public partial class IngredientEditForm
    {
        [Inject]
        public IngredientFacade IngredientFacade { get; set; } = null!;

        [Parameter]
        public Guid Id { get; init; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        public IngredientDetailModel Data { get; set; } = GetNewIngredientModel();

        private EditContext? editContext;

        // Property to store general error messages that aren't tied to specific fields
        public List<string> GeneralErrorMessages { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                Data = await IngredientFacade.GetByIdAsync(Id);
            }

            editContext = new EditContext(Data);
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            // Recreate EditContext when Data changes (e.g., after loading from API)
            if (editContext?.Model != Data)
            {
                editContext = new EditContext(Data);
            }
            await base.OnParametersSetAsync();
        }

        public async Task Save()
        {
            GeneralErrorMessages.Clear();

            if (editContext == null)
            {
                return;
            }

            if (!editContext.Validate())
            {
                // Blazored.FluentValidation automatically handles client-side validation
                return;
            }

            try
            {
                await IngredientFacade.SaveAsync(Data);

                Data = GetNewIngredientModel();
                editContext = new EditContext(Data);
                await NotifyOnModification();
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
                AddGeneralError(string.Format(IngredientEditFormResources.ErrorMessage_General, ex.Message));
            }
        }

        public async Task Delete()
        {
            GeneralErrorMessages.Clear();

            try
            {
                await IngredientFacade.DeleteAsync(Id);
                await NotifyOnModification();
            }
            catch (Exception ex)
            {
                AddGeneralError(string.Format(IngredientEditFormResources.ErrorMessage_FailedToDelete, ex.Message));
            }
        }

        private async Task NotifyOnModification()
        {
            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }

        private static IngredientDetailModel GetNewIngredientModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
            };

        private void AddGeneralError(string message)
        {
            GeneralErrorMessages.Add(message);
            StateHasChanged();
        }

        private void HandleValidationErrors(HttpValidationProblemDetails validationProblem)
        {
            if (editContext == null || validationProblem?.Errors == null) return;

            var messages = new ValidationMessageStore(editContext);
            messages.Clear();

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
                        messages.Add(fieldIdentifier, message);
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
                AddGeneralError(IngredientEditFormResources.ErrorMessage_General);
                return;
            }

            // Try to parse validation errors from the response content
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

            AddGeneralError(IngredientEditFormResources.ErrorMessage_General);
        }
    }
}
