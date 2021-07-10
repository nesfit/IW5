using CookBook.Models;

namespace CookBook.DAL.Web.Repositories
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