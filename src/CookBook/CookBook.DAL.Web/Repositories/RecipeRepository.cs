using CookBook.Models;

namespace CookBook.DAL.Web.Repositories
{
    public class RecipeRepository : RepositoryBase<RecipeDetailModel>
    {
        public override string TableName { get; } = "recipes";

        public RecipeRepository(LocalDb localDb)
            : base(localDb)
        {
        }
    }
}