using System.Net.Http.Json;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Components
{
    public partial class IngredientEditForm
    {
        [Inject]
        private HttpClient httpClient { get; set; } = null!;

        [Parameter]
        public Guid Id { get; set; }

        public IngredientDetailModel Data { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (Id == Guid.Empty)
            {
                Data = new IngredientDetailModel();
            }
            else
            {
                var dataList = await httpClient.GetFromJsonAsync<List<IngredientDetailModel>>("sample-data/ingredients.json");
                if (dataList is not null)
                {
                    Data = dataList.Single(item => item.Id == Id);
                }
            }

            await base.OnInitializedAsync();
        }
    }
}
