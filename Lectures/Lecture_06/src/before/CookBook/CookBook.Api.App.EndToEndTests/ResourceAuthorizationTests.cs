using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Encodings.Web;
using CookBook.Api.App.Authorization;
using CookBook.Common;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;

namespace CookBook.Api.App.EndToEndTests;

public class ResourceAuthorizationTests : IAsyncDisposable
{
    private readonly AuthorizedApiApplicationFactory application = new();

    [Fact]
    public async Task OwnerCanUpdateButNotDeleteOwnIngredient()
    {
        var owner = CreateClient("owner");
        var ingredient = NewIngredient();
        await CreateIngredientAsync(owner, ingredient);

        var update = await owner.PutAsJsonAsync("/api/ingredient", ingredient with { Name = "Updated" });
        var delete = await owner.DeleteAsync($"/api/ingredient/{ingredient.Id}");

        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, delete.StatusCode);
    }

    [Fact]
    public async Task AdminCanDeleteIngredient()
    {
        var ingredient = NewIngredient();
        await CreateIngredientAsync(CreateClient("owner"), ingredient);

        var delete = await CreateClient("admin", UserRoles.Admin).DeleteAsync($"/api/ingredient/{ingredient.Id}");

        Assert.Equal(HttpStatusCode.OK, delete.StatusCode);
    }

    [Fact]
    public async Task OwnerCanUpdateAndDeleteOwnRecipe()
    {
        var owner = CreateClient("owner");
        var recipe = NewRecipe();
        (await owner.PostAsJsonAsync("/api/recipe", recipe)).EnsureSuccessStatusCode();

        var update = await owner.PutAsJsonAsync("/api/recipe", recipe with { Name = "Updated" });
        var delete = await owner.DeleteAsync($"/api/recipe/{recipe.Id}");

        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        Assert.Equal(HttpStatusCode.OK, delete.StatusCode);
    }

    [Fact]
    public async Task CreatingRecipePersistsIngredientAmounts()
    {
        var owner = CreateClient("owner");
        var ingredient = NewIngredient();
        await CreateIngredientAsync(owner, ingredient);
        var recipe = NewRecipe() with
        {
            IngredientAmounts =
            [
                new RecipeDetailIngredientModel
                {
                    Id = Guid.NewGuid(),
                    Amount = 2,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel { Id = ingredient.Id, Name = ingredient.Name }
                }
            ]
        };

        (await owner.PostAsJsonAsync("/api/recipe", recipe)).EnsureSuccessStatusCode();
        var saved = await owner.GetFromJsonAsync<RecipeDetailModel>($"/api/recipe/{recipe.Id}");

        var amount = Assert.Single(Assert.IsType<RecipeDetailModel>(saved).IngredientAmounts);
        Assert.Equal(2, amount.Amount);
        Assert.Equal(ingredient.Id, amount.Ingredient.Id);
    }

    [Fact]
    public async Task CreatingResourcesWithExistingIdsReturnsConflict()
    {
        var owner = CreateClient("owner");
        var other = CreateClient("other");
        var ingredient = NewIngredient();
        var recipe = NewRecipe();
        await CreateIngredientAsync(owner, ingredient);
        (await owner.PostAsJsonAsync("/api/recipe", recipe)).EnsureSuccessStatusCode();

        var duplicateIngredient = await other.PostAsJsonAsync("/api/ingredient", ingredient);
        var duplicateRecipe = await other.PostAsJsonAsync("/api/recipe", recipe);

        Assert.Equal(HttpStatusCode.Conflict, duplicateIngredient.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, duplicateRecipe.StatusCode);
    }

    [Fact]
    public async Task UpdatingRecipeCannotReuseIngredientAmountFromAnotherRecipe()
    {
        var owner = CreateClient("owner");
        var ingredient = NewIngredient();
        await CreateIngredientAsync(owner, ingredient);
        var victimRecipe = NewRecipe() with
        {
            IngredientAmounts =
            [
                new RecipeDetailIngredientModel
                {
                    Id = Guid.NewGuid(),
                    Amount = 2,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel { Id = ingredient.Id, Name = ingredient.Name }
                }
            ]
        };
        var editedRecipe = NewRecipe();
        (await owner.PostAsJsonAsync("/api/recipe", victimRecipe)).EnsureSuccessStatusCode();
        (await owner.PostAsJsonAsync("/api/recipe", editedRecipe)).EnsureSuccessStatusCode();
        var persistedVictim = await owner.GetFromJsonAsync<RecipeDetailModel>($"/api/recipe/{victimRecipe.Id}");

        var attempt = await owner.PutAsJsonAsync("/api/recipe", editedRecipe with
        {
            IngredientAmounts = Assert.IsType<RecipeDetailModel>(persistedVictim).IngredientAmounts
        });

        Assert.Equal(HttpStatusCode.BadRequest, attempt.StatusCode);
        var stillOwned = await owner.GetFromJsonAsync<RecipeDetailModel>($"/api/recipe/{victimRecipe.Id}");
        Assert.Single(Assert.IsType<RecipeDetailModel>(stillOwned).IngredientAmounts);
    }

    [Theory]
    [InlineData("post")]
    [InlineData("put")]
    [InlineData("upsert")]
    public async Task RecipeEndpointsRejectEmptyIngredientAmountIds(string operation)
    {
        var owner = CreateClient("owner");
        var recipe = NewRecipe();
        if (operation != "post")
        {
            (await owner.PostAsJsonAsync("/api/recipe", recipe)).EnsureSuccessStatusCode();
        }

        var invalidRecipe = recipe with
        {
            IngredientAmounts =
            [
                new RecipeDetailIngredientModel
                {
                    Id = Guid.Empty,
                    Amount = 1,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel { Id = Guid.NewGuid(), Name = "Ingredient" }
                }
            ]
        };

        var response = await SendRecipeWriteAsync(owner, operation, invalidRecipe);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("post")]
    [InlineData("put")]
    [InlineData("upsert")]
    public async Task RecipeEndpointsRejectDuplicateIngredientAmountIds(string operation)
    {
        var owner = CreateClient("owner");
        var recipe = NewRecipe();
        if (operation != "post")
        {
            (await owner.PostAsJsonAsync("/api/recipe", recipe)).EnsureSuccessStatusCode();
        }

        var amountId = Guid.NewGuid();
        var invalidRecipe = recipe with
        {
            IngredientAmounts =
            [
                new RecipeDetailIngredientModel
                {
                    Id = amountId,
                    Amount = 1,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel { Id = Guid.NewGuid(), Name = "Ingredient A" }
                },
                new RecipeDetailIngredientModel
                {
                    Id = amountId,
                    Amount = 2,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel { Id = Guid.NewGuid(), Name = "Ingredient B" }
                }
            ]
        };

        var response = await SendRecipeWriteAsync(owner, operation, invalidRecipe);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task OtherUserCannotDeleteRecipe()
    {
        var recipe = NewRecipe();
        (await CreateClient("owner").PostAsJsonAsync("/api/recipe", recipe)).EnsureSuccessStatusCode();

        var delete = await CreateClient("other").DeleteAsync($"/api/recipe/{recipe.Id}");

        Assert.Equal(HttpStatusCode.Forbidden, delete.StatusCode);
    }

    [Fact]
    public async Task OtherUserCannotUpdateUpsertOrDeleteIngredient()
    {
        var ingredient = NewIngredient();
        await CreateIngredientAsync(CreateClient("owner"), ingredient);
        var other = CreateClient("other");

        var update = await other.PutAsJsonAsync("/api/ingredient", ingredient with { Name = "Stolen" });
        var upsert = await other.PostAsJsonAsync("/api/ingredient/upsert", ingredient with { Name = "Stolen" });
        var delete = await other.DeleteAsync($"/api/ingredient/{ingredient.Id}");

        Assert.Equal(HttpStatusCode.Forbidden, update.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, upsert.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, delete.StatusCode);
    }

    [Fact]
    public async Task AdminUpdateDoesNotTakeIngredientOwnership()
    {
        var owner = CreateClient("owner");
        var ingredient = NewIngredient();
        await CreateIngredientAsync(owner, ingredient);

        var adminUpdate = await CreateClient("admin", UserRoles.Admin)
            .PutAsJsonAsync("/api/ingredient", ingredient with { Name = "Admin edit" });
        var ownerUpdate = await owner.PutAsJsonAsync("/api/ingredient", ingredient with { Name = "Owner edit" });

        Assert.Equal(HttpStatusCode.OK, adminUpdate.StatusCode);
        Assert.Equal(HttpStatusCode.OK, ownerUpdate.StatusCode);
    }

    [Fact]
    public async Task RecipeUpdateKeepsOwner()
    {
        var owner = CreateClient("owner");
        var recipe = NewRecipe();
        (await owner.PostAsJsonAsync("/api/recipe", recipe)).EnsureSuccessStatusCode();

        var firstUpdate = await owner.PutAsJsonAsync("/api/recipe", recipe with { Name = "First" });
        var secondUpdate = await owner.PutAsJsonAsync("/api/recipe", recipe with { Name = "Second" });
        var otherUpdate = await CreateClient("other").PutAsJsonAsync("/api/recipe", recipe with { Name = "Stolen" });

        Assert.Equal(HttpStatusCode.OK, firstUpdate.StatusCode);
        Assert.Equal(HttpStatusCode.OK, secondUpdate.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, otherUpdate.StatusCode);
    }

    [Fact]
    public async Task AdminCanDeleteOtherUsersRecipe()
    {
        var recipe = NewRecipe();
        (await CreateClient("owner").PostAsJsonAsync("/api/recipe", recipe)).EnsureSuccessStatusCode();

        var delete = await CreateClient("admin", UserRoles.Admin).DeleteAsync($"/api/recipe/{recipe.Id}");

        Assert.Equal(HttpStatusCode.OK, delete.StatusCode);
    }

    [Fact]
    public async Task MissingRecordsReturnNotFound()
    {
        var client = CreateClient("owner");

        var ingredientUpdate = await client.PutAsJsonAsync("/api/ingredient", NewIngredient());
        var ingredientDelete = await client.DeleteAsync($"/api/ingredient/{Guid.NewGuid()}");
        var recipeUpdate = await client.PutAsJsonAsync("/api/recipe", NewRecipe());
        var recipeDelete = await client.DeleteAsync($"/api/recipe/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, ingredientUpdate.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, ingredientDelete.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, recipeUpdate.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, recipeDelete.StatusCode);
    }

    [Fact]
    public async Task AuthenticatedUserWithoutIdentifierCannotModifyOwnedRecord()
    {
        var ingredient = NewIngredient();
        await CreateIngredientAsync(CreateClient("owner"), ingredient);

        var update = await CreateClient(userId: null)
            .PutAsJsonAsync("/api/ingredient", ingredient with { Name = "No id" });

        Assert.Equal(HttpStatusCode.Forbidden, update.StatusCode);
    }

    [Fact]
    public async Task SameSubjectFromDifferentIssuerIsNotOwner()
    {
        var recipe = NewRecipe();
        (await CreateClient("shared-subject", issuer: "Entra").PostAsJsonAsync("/api/recipe", recipe))
            .EnsureSuccessStatusCode();

        var impostor = CreateClient("shared-subject", issuer: "CookBookIdentityProvider");
        var update = await impostor.PutAsJsonAsync("/api/recipe", recipe with { Name = "Stolen" });
        var delete = await impostor.DeleteAsync($"/api/recipe/{recipe.Id}");

        Assert.Equal(HttpStatusCode.Forbidden, update.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, delete.StatusCode);
    }

    [Fact]
    public async Task AnonymousRecipeUpsertRequiresAuthentication()
    {
        var response = await application.CreateClient().PostAsJsonAsync("/api/recipe/upsert", NewRecipe());

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    public async ValueTask DisposeAsync() => await application.DisposeAsync();

    private HttpClient CreateClient(string? userId, string? role = null, string? issuer = null)
    {
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Add(TestAuthenticationHandler.UserHeader, userId ?? TestAuthenticationHandler.NoUserId);
        if (role is not null)
        {
            client.DefaultRequestHeaders.Add(TestAuthenticationHandler.RoleHeader, role);
        }
        if (issuer is not null)
        {
            client.DefaultRequestHeaders.Add(TestAuthenticationHandler.IssuerHeader, issuer);
        }
        return client;
    }

    private static async Task CreateIngredientAsync(HttpClient client, IngredientDetailModel ingredient)
        => (await client.PostAsJsonAsync("/api/ingredient", ingredient)).EnsureSuccessStatusCode();

    private static Task<HttpResponseMessage> SendRecipeWriteAsync(
        HttpClient client,
        string operation,
        RecipeDetailModel recipe)
        => operation switch
        {
            "post" => client.PostAsJsonAsync("/api/recipe", recipe),
            "put" => client.PutAsJsonAsync("/api/recipe", recipe),
            "upsert" => client.PostAsJsonAsync("/api/recipe/upsert", recipe),
            _ => throw new ArgumentOutOfRangeException(nameof(operation))
        };

    private static IngredientDetailModel NewIngredient() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Ingredient",
        Description = "Description",
        ImageUrl = "https://example.com/ingredient.jpg"
    };

    private static RecipeDetailModel NewRecipe() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Recipe",
        Description = "Description",
        ImageUrl = "https://example.com/recipe.jpg",
        Duration = TimeSpan.FromMinutes(5),
        FoodType = FoodType.Dessert,
        IngredientAmounts = []
    };

    private sealed class AuthorizedApiApplicationFactory : CookBookApiApplicationFactory
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.PostConfigure<CookBook.Common.Options.IdentityOptions>(options => options.IsEnabled = true);
                services.AddAuthentication()
                    .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(TestAuthenticationHandler.SchemeName, _ => { });
                services.PostConfigure<AuthenticationOptions>(options =>
                {
                    options.DefaultScheme = TestAuthenticationHandler.SchemeName;
                    options.DefaultAuthenticateScheme = TestAuthenticationHandler.SchemeName;
                    options.DefaultChallengeScheme = TestAuthenticationHandler.SchemeName;
                    options.DefaultForbidScheme = TestAuthenticationHandler.SchemeName;
                });
            });
        }
    }

    private sealed class TestAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        public const string SchemeName = "Test";
        public const string UserHeader = "X-Test-User";
        public const string RoleHeader = "X-Test-Role";
        public const string IssuerHeader = "X-Test-Issuer";
        public const string DefaultIssuer = "CookBookIdentityProvider";
        public const string NoUserId = "<none>";

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(UserHeader, out var userId))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var claims = new List<Claim>();
            if (userId != NoUserId)
            {
                var issuer = Request.Headers.TryGetValue(IssuerHeader, out var issuerHeader)
                    ? issuerHeader.ToString()
                    : DefaultIssuer;
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
                claims.Add(new Claim(
                    ResourceOwnerClaims.OwnerIdClaimType,
                    ResourceOwnerClaims.CreateOwnerId(issuer, userId.ToString())));
            }
            if (Request.Headers.TryGetValue(RoleHeader, out var role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, SchemeName));
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, SchemeName)));
        }
    }
}
