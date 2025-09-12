using CookBook.Common.Models;

namespace CookBook.Web.DAL.Repositories
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
