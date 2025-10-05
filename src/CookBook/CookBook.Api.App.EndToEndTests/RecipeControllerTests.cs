using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CookBook.Common.Models;
using Xunit;

namespace CookBook.Api.App.EndToEndTests
{
    public class RecipeControllerTests : IAsyncDisposable
    {
        private readonly CookBookApiApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public RecipeControllerTests()
        {
            application = new CookBookApiApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllRecipes_Returns_At_Last_One_Recipe()
        {
            var response = await client.Value.GetAsync("/api/Recipe");

            response.EnsureSuccessStatusCode();

            var recipes = await response.Content.ReadFromJsonAsync<ICollection<RecipeListModel>>();
            Assert.NotNull(recipes);
            Assert.NotEmpty(recipes);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
