using System;
using System.Threading.Tasks;
using CookBook.Common.Models;
using CookBook.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

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

        public IngredientDetailModel Data { get; set; } = new ();

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                Data = await IngredientFacade.GetByIdAsync(Id);
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            await IngredientFacade.SaveAsync(Data);
            Data = new();
            await NotifyOnModification();
        }

        public async Task Delete()
        {
            await IngredientFacade.DeleteAsync(Id);
            await NotifyOnModification();
        }

        private async Task NotifyOnModification()
        {
            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }
    }
}
