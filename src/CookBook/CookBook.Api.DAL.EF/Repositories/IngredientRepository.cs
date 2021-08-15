using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class IngredientRepository : RepositoryBase<IngredientEntity>, IIngredientRepository
    {
        public IngredientRepository(IDbContextFactory<CookBookDbContext> dbContextFactory)
            : base(dbContextFactory)
        {
        }
    }
}
