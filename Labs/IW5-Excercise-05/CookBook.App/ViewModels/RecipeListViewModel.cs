using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CookBook.App.Commands;
using CookBook.BL;
using CookBook.BL.Messages;
using CookBook.BL.Models;
using CookBook.BL.Repositories;

namespace CookBook.App.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        private ObservableCollection<RecipeListModel> _recipes;
        private readonly RecipeRepository _recipeRepository;
        private readonly IMessenger _messenger;

        public ObservableCollection<RecipeListModel> Recipes
        {
            get { return _recipes; }
            set
            {
                if (Equals(value, _recipes)) return;
                _recipes = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectRecipeCommand { get; }

        public RecipeListViewModel(RecipeRepository recipeRepository, IMessenger messenger)
        {
            _recipeRepository = recipeRepository;
            _messenger = messenger;

            _messenger.Register<DeletedRecipeMessage>(DeletedRecipeMessageReceived);
            _messenger.Register<UpdatedRecipeMessage>((p) => OnLoad());
            SelectRecipeCommand = new RelayCommand(RecipeSelectionChanged);
        }

        public void OnLoad()
        {
            Recipes = new ObservableCollection<RecipeListModel>(_recipeRepository.GetAll());
        }

        public void RecipeSelectionChanged(object parameter)
        {
            var recipeId = (RecipeListModel)parameter;

            if (recipeId == null)
            {
                return;
            }

            _messenger.Send(new SelectedRecipeMessage() { Id = recipeId.Id });
        }

        private void DeletedRecipeMessageReceived(DeletedRecipeMessage message)
        {
            var deletedRecipe = Recipes.FirstOrDefault(r => r.Id == message.RecipeId);
            if (deletedRecipe != null)
            {
                Recipes.Remove(deletedRecipe);
            }
        }
    }
}