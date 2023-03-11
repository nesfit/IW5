using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class IngredientAmountRepository : RepositoryBase<IngredientAmountEntity>, IIngredientAmountRepository
    {
        public IngredientAmountRepository(CookBookDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
