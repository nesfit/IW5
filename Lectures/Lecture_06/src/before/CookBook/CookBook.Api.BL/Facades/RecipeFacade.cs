using CookBook.Api.BL.Mappers;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class RecipeFacade(
    IRecipeRepository recipeRepository,
    RecipeMapper mapper)
            : FacadeBase<IRecipeRepository, RecipeEntity>(recipeRepository),
    IRecipeFacade
{
    private readonly IRecipeRepository recipeRepository = recipeRepository;
    private readonly RecipeMapper mapper = mapper;

    public List<RecipeListModel> GetAll()
    {
        var recipeEntities = recipeRepository.GetAll();
        return mapper.ToListModels(recipeEntities);
    }

    public RecipeDetailModel? GetById(Guid id)
    {
        var recipeEntity = recipeRepository.GetById(id);
        return recipeEntity == null
            ? null
            : mapper.ToDetailModel(recipeEntity);
    }

    public Guid Create(RecipeDetailModel recipeModel, string? ownerId)
    {
        MergeIngredientAmounts(recipeModel);
        var recipeEntity = mapper.ToEntity(recipeModel, ownerId);
        recipeEntity.IngredientAmounts = ToIngredientAmountEntities(recipeModel, recipeEntity.Id, new HashSet<Guid>());
        return recipeRepository.Insert(recipeEntity);
    }

    public Guid? Update(RecipeDetailModel recipeModel)
    {
        MergeIngredientAmounts(recipeModel);

        var existingIngredientAmountIds = recipeRepository.GetById(recipeModel.Id)?.IngredientAmounts
            .Select(ingredientAmount => ingredientAmount.Id)
            .ToHashSet() ?? new HashSet<Guid>();

        var recipeEntity = mapper.ToEntity(recipeModel);
        recipeEntity.IngredientAmounts = ToIngredientAmountEntities(recipeModel, recipeEntity.Id, existingIngredientAmountIds);
        var result = recipeRepository.Update(recipeEntity);
        return result;
    }

    // Client-supplied ingredient amount ids are kept only if they already belong to this recipe;
    // anything else gets a fresh id so a request can never address another recipe's ingredient amounts.
    private static List<IngredientAmountEntity> ToIngredientAmountEntities(
        RecipeDetailModel recipeModel,
        Guid recipeId,
        IReadOnlySet<Guid> existingIngredientAmountIds)
        => recipeModel.IngredientAmounts.Select(ingredientAmount =>
            new IngredientAmountEntity
            {
                Id = existingIngredientAmountIds.Contains(ingredientAmount.Id) ? ingredientAmount.Id : Guid.NewGuid(),
                Amount = ingredientAmount.Amount,
                Unit = ingredientAmount.Unit,
                RecipeId = recipeId,
                IngredientId = ingredientAmount.Ingredient.Id
            }).ToList();

    public bool HasIngredientAmountOwnedByAnotherRecipe(RecipeDetailModel recipeModel)
        => recipeModel.IngredientAmounts.Any(amount =>
            recipeRepository.IngredientAmountBelongsToDifferentRecipe(amount.Id, recipeModel.Id));

    public bool HasInvalidIngredientAmountIds(RecipeDetailModel recipeModel)
    {
        var ids = recipeModel.IngredientAmounts
            .Select(amount => amount.Id)
            .Where(id => id != Guid.Empty)
            .ToList();
        return ids.Count != ids.Distinct().Count();
    }

    public void MergeIngredientAmounts(RecipeDetailModel recipe)
    {
        var result = new List<RecipeDetailIngredientModel>();
        var ingredientAmountGroups = recipe.IngredientAmounts.GroupBy(t => $"{t.Ingredient.Id}-{t.Unit}");

        foreach (var ingredientAmountGroup in ingredientAmountGroups)
        {
            var ingredientAmountFirst = ingredientAmountGroup.First();
            var totalAmount = ingredientAmountGroup.Sum(t => t.Amount);
            var ingredientAmount = ingredientAmountFirst with { Amount = totalAmount };

            result.Add(ingredientAmount);
        }

        recipe.IngredientAmounts = result;
    }

    public void Delete(Guid id)
    {
        recipeRepository.Remove(id);
    }
}
