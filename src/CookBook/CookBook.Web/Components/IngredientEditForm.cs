using System;
using System.Threading.Tasks;
using CookBook.BL.Web.Facades;
using CookBook.Models;
using Microsoft.AspNetCore.Components;

namespace CookBook.App.Web
{
    public partial class IngredientEditForm
    {
        [Inject]
        public IngredientFacade IngredientFacade { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        public IngredientDetailModel Data { get; set; } = new IngredientDetailModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id == Guid.Empty)
            {
                Data = new IngredientDetailModel();
            }
            else
            {
                Data = await IngredientFacade.GetByIdAsync(Id);
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            await IngredientFacade.SaveAsync(Data);
            Data = new IngredientDetailModel();

            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }

        public async Task Delete()
        {
            await IngredientFacade.DeleteAsync(Id);

            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }
    }
}