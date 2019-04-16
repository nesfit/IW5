using CookBook.App.Services;
using CookBook.BL.Interfaces;
using CookBook.BL.Repositories;
using CookBook.BL.Services;
using CookBook.DAL.Factories;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CookBook.App.ViewModels
{
    public class ViewModelLocator
    {
        private readonly IMediator mediator;
        private readonly IIngredientRepository ingredientRepository;
        private readonly IMessageBoxService messageBoxService;

        public IngredientListViewModel IngredientListViewModel => new IngredientListViewModel(ingredientRepository, mediator);
        public IngredientDetailViewModel IngredientDetailViewModel => new IngredientDetailViewModel(ingredientRepository, messageBoxService, mediator);

        public ViewModelLocator()
        {
            mediator = new Mediator();
            IDbContextFactory dbContextFactory = new DbContextFactory();
#if DEBUG
            using (var dbx = dbContextFactory.CreateDbContext())
            {
                dbx.Database.EnsureCreated();
            }
#endif

            ingredientRepository = new IngredientRepository(dbContextFactory);
            messageBoxService = new MessageBoxService();
        }
    }
}
