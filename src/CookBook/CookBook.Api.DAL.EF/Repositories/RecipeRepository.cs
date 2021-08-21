using System;
using System.Collections.Generic;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class RecipeRepository : RepositoryBase<RecipeEntity>, IRecipeRepository
    {
        public RecipeRepository(CookBookDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
