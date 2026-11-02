using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class IngredientRepository : RepositoryBase<IngredientEntity>, IIngredientRepository
    {
        public IngredientRepository(CookBookDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
