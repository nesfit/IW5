using CookBook.Api.Tests.Fixtures;
using CookBook.BL.Api.Models.Ingredient;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CookBook.Api.Tests
{
    public class IngredientControllerTests : IntegrationTest
    {
        public IngredientControllerTests(ApiWebApplicationFactory fixture)
            : base(fixture)
        {
        }

        [Theory]
        [InlineData("api/ingredient")]
        [InlineData("api/ingredient/df935095-8709-4040-a2bb-b6f97cb416dc")]
        public async Task SmokeTest_Should_ResultInOK(string endpoint)
        {
            // Act
            var response = await _client.GetAsync(endpoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_Should_Return_Two_Ingredients()
        {
            // Act
            var response = await _client.GetAsync("api/ingredient");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var ingredients = JsonConvert.DeserializeObject<List<IngredientListModel>>(await response.Content.ReadAsStringAsync());
            ingredients.Should().HaveCount(2);

            ingredients[0].Name.Should().Be("Vejce");
            ingredients[1].Name.Should().Be("Cibule");
        }

        [Theory]
        [InlineData("df935095-8709-4040-a2bb-b6f97cb416dc", "Vejce", "Popis vajec")]
        [InlineData("23b3902d-7d4f-4213-9cf0-112348f56238", "Cibule", "Popis cibule")]
        public async Task GetById_Should_Return_Specified_Ingredient(string id, string name, string description)
        {
            // Act
            var response = await _client.GetAsync($"api/ingredient/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var ingredientDetail = JsonConvert.DeserializeObject<IngredientDetailModel>(await response.Content.ReadAsStringAsync());
            ingredientDetail.Id.Should().Be(id);
            ingredientDetail.Name.Should().Be(name);
            ingredientDetail.Description.Should().Be(description);
        }

        [Fact]
        public async Task Create_Should_Create_New_Ingredient()
        {
            // Arrange
            var newIngredient = new IngredientNewModel
            {
                Name = "Mléko",
                Description = "Mléko polotučné"
            };
            var newIngredientSerialized = JsonConvert.SerializeObject(newIngredient);
            var stringContent = new StringContent(newIngredientSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/ingredient", stringContent);
            //var response = await _client.PostAsJsonAsync("api/ingredient", newIngredient); // Microsoft.AspNet.WebApi.Client NuGet package needs to be installed.

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newIngredientGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newIngredientGuid.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData("Va")]
        [InlineData("Protože se slaninou je všechno lepší. Protože se slaninou je všechno lepší. Protože se slaninou je všechno lepší. Protože se slaninou je všechno lepší.")]
        public async Task Create_With_Invalid_Name_Returns_Expected_Validation_Error(string name)
        {
            // Arrange
            var newIngredient = new IngredientNewModel
            {
                Name = name,
                Description = "Mléko plnotučné"
            };
            var newIngredientSerialized = JsonConvert.SerializeObject(newIngredient);
            var stringContent = new StringContent(newIngredientSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/ingredient?version=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            //var serializableError = JsonConvert.DeserializeObject<SerializableError>(await response.Content.ReadAsStringAsync());
            var validationProblemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(await response.Content.ReadAsStringAsync());
            validationProblemDetails.Errors.Should().HaveCount(1);
            validationProblemDetails.Errors["Name"][0].Should().Be("Ingredient name length must be between 3 and 100 characters");
            validationProblemDetails.Title.Should().Be("One or more validation errors occurred.");
        }

        [Fact]
        public async Task Create_With_Empty_Name_Returns_Expected_Validation_Error()
        {
            // Arrange
            var newIngredient = new IngredientNewModel
            {
                Name = "",
                Description = "Mléko plnotučné"
            };
            var newIngredientSerialized = JsonConvert.SerializeObject(newIngredient);
            var stringContent = new StringContent(newIngredientSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/ingredient?version=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            //var serializableError = JsonConvert.DeserializeObject<SerializableError>(await response.Content.ReadAsStringAsync());
            var validationProblemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(await response.Content.ReadAsStringAsync());
            validationProblemDetails.Errors.Should().HaveCount(1);
            validationProblemDetails.Errors["Name"][0].Should().Be("Ingredient name is required");
            validationProblemDetails.Title.Should().Be("One or more validation errors occurred.");
        }

        [Fact]
        public async Task Create_With_Invalid_Description_Returns_Expected_Validation_Error()
        {
            // Arrange
            var newIngredient = new IngredientNewModel
            {
                Name = "Mléko",
                Description = "Popis"
            };
            var newIngredientSerialized = JsonConvert.SerializeObject(newIngredient);
            var stringContent = new StringContent(newIngredientSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/ingredient?version=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var validationProblemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(await response.Content.ReadAsStringAsync());
            validationProblemDetails.Errors.Should().HaveCount(1);
            validationProblemDetails.Errors["Description"][0].Should().Be("Ingredient description length must be at least 10 characters");
            validationProblemDetails.Title.Should().Be("One or more validation errors occurred.");
        }

        [Fact]
        public async Task Update_Should_Update_Existing_Ingredient()
        {
            // Arrange
            Guid ingredientToUpdateGuid = new Guid("df935095-8709-4040-a2bb-b6f97cb416dc");
            var ingredientUpdateModel = new IngredientUpdateModel
            {
                Id = ingredientToUpdateGuid,
                Name = "Velké vejce",
                Description = "Nový popis"
            };
            var ingredientUpdateModelSerialized = JsonConvert.SerializeObject(ingredientUpdateModel);
            var stringContent = new StringContent(ingredientUpdateModelSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/ingredient", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var ingredientGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            ingredientGuid.Should().Be(ingredientToUpdateGuid);
        }

        [Fact]
        public async Task Delete_Of_Existing_Ingredient_Should_ResultInOK()
        {
            // Arrage
            var ingredientToDeleteGuid = new Guid("23b3902d-7d4f-4213-9cf0-112348f56238");

            // Act
            var response = await _client.DeleteAsync($"api/ingredient/{ingredientToDeleteGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_Using_Not_Valid_Ingredient_Id_Should_ResultInBadRequest()
        {
            // Act
            var response = await _client.DeleteAsync($"api/ingredient/blabla");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_Of_NonExisting_Ingredient_Id_Should_ResultInInternalServerError()
        {
            // Arrage
            var nonExistingIngredientGuid = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"api/ingredient/{nonExistingIngredientGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}
