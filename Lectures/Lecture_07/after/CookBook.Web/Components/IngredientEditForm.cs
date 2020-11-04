using CookBook.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CookBook.WEB.BL.Facades;

namespace CookBook.Web.Components
{
    public partial class IngredientEditForm
    {
        [Inject]
        public IngredientsFacade IngredientFacade { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        public IngredientDetailModel Data { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id == Guid.Empty)
            {
                Data = new IngredientDetailModel();
            }
            else
            {
                Data =await IngredientFacade.GetIngredientAsync(Id);
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            var resultId = await IngredientFacade.SaveAsync(Data);
            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(resultId);
            }
            Data = new IngredientDetailModel();
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