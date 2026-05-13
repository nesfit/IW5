namespace CookBook.Web.BL.Api;

public partial class IngredientApiClient
{
    partial void Initialize()
    {
        ReadResponseAsString = true;
    }
}

public partial class RecipeApiClient
{
    partial void Initialize()
    {
        ReadResponseAsString = true;
    }
}
