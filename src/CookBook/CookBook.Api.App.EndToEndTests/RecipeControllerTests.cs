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

        [Fact]
        public async Task IngredientUpsert_WithEmptyName_ShouldReturnValidationError()
        {
            // Arrange
            var invalidIngredient = new
            {
                id = Guid.Empty,
                name = "",
                description = "This is a valid description that is longer than 10 characters",
                imageUrl = "https://example.com/image.jpg"
            };

            var json = JsonSerializer.Serialize(invalidIngredient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await client.Value.PostAsync("/api/ingredient/upsert", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Name", responseContent);
        }

        [Fact]
        public async Task IngredientUpsert_WithShortDescription_ShouldReturnValidationError()
        {
            // Arrange
            var invalidIngredient = new
            {
                id = Guid.Empty,
                name = "Valid Name",
                description = "Short",
                imageUrl = "https://example.com/image.jpg"
            };

            var json = JsonSerializer.Serialize(invalidIngredient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await client.Value.PostAsync("/api/ingredient/upsert", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Description", responseContent);
        }

        [Fact]
        public async Task IngredientUpsert_WithValidData_ShouldSucceed()
        {
            // Arrange
            var validIngredient = new IngredientDetailModel
            {
                Id = Guid.Empty,
                Name = "Valid Ingredient Name",
                Description = "This is a valid description that is longer than 10 characters",
                ImageUrl = "https://example.com/image.jpg"
            };

            // Act
            var response = await client.Value.PostAsJsonAsync("/api/ingredient/upsert", validIngredient);

            // Assert
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Expected OK but got {response.StatusCode}. Response: {responseContent}");
            }
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var createdId = await response.Content.ReadFromJsonAsync<Guid>();
            Assert.NotEqual(Guid.Empty, createdId);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
