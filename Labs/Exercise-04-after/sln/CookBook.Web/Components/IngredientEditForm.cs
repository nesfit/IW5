using CookBook.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CookBook.Web.Components
{
    public partial class IngredientEditForm
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        public IngredientDetailModel Data { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id == Guid.Empty)
            {
                Data = new IngredientDetailModel();
            }
            else
            {
                var dataList = await httpClient.GetFromJsonAsync<List<IngredientDetailModel>>("sample-data/ingredients.json");
                Data = dataList.Single(item => item.Id == Id);
            }

            await base.OnInitializedAsync();
        }
    }
}