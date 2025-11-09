using CookBook.Common.Models;
using CookBook.Web.App.Resources.Texts;
using CookBook.Web.BL.Api;
using CookBook.Web.BL.Facades;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
        private ValidationMessageStore? validationMessageStore;

        // Property to store general error messages that aren't tied to specific fields
        public List<string> GeneralErrorMessages { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                Data = await IngredientFacade.GetByIdAsync(Id);
            }

            editContext = new EditContext(Data);
            validationMessageStore = new ValidationMessageStore(editContext);
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            // Recreate EditContext when Data changes (e.g., after loading from API)
            if (editContext?.Model != Data)
            {
                editContext = new EditContext(Data);
                validationMessageStore = new ValidationMessageStore(editContext);
            }
            await base.OnParametersSetAsync();
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
                // Blazored.FluentValidation automatically handles client-side validation
                return;
            }

            try
            {
                await IngredientFacade.SaveAsync(Data);

                Data = GetNewIngredientModel();
                editContext = new EditContext(Data);
                validationMessageStore = new ValidationMessageStore(editContext);
                await NotifyOnModification();
            }
            catch (ApiException<HttpValidationProblemDetails> ex)
            {
                HandleValidationErrors(ex.Result);
            }
            catch (ApiException ex) when (ex.StatusCode
                                              is StatusCodes.Status400BadRequest
                                              or StatusCodes.Status403Forbidden)
            {
                HandleGenericError(ex);
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

        private void HandleGenericError(ApiException ex)
        {
            if (editContext == null)
            {
                AddGeneralError(IngredientEditFormResources.ErrorMessage_General);
                return;
            }

            // Try to parse error details from the response content
            try
            {
                if (!string.IsNullOrEmpty(ex.Response))
                {
                    var httpValidationProblemDetailsResponse = JsonConvert.DeserializeObject<HttpValidationProblemDetails>(ex.Response);

                    if (httpValidationProblemDetailsResponse?.Errors is not null)
                    {
                        HandleValidationErrors(httpValidationProblemDetailsResponse);
                        return;
                    }

                    var httpProblemDetailsResponse = JsonConvert.DeserializeObject<HttpProblemDetails>(ex.Response);
                    if (httpProblemDetailsResponse?.Detail is not null)
                    {
                        AddGeneralError(httpProblemDetailsResponse.Detail);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                // If deserialization fails, fall back to general error handling
                AddGeneralError(IngredientEditFormResources.ErrorMessage_General);
            }
        }
    }
}
