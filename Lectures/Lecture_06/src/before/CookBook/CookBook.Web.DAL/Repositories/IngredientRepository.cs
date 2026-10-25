using CookBook.Common.Models;

namespace CookBook.Web.DAL.Repositories
{
    public class IngredientRepository : RepositoryBase<IngredientDetailModel>
    {
        public override string TableName { get; } = "ingredients";

        public IngredientRepository(LocalDb localDb)
            : base(localDb)
        {
        }
    }
}
