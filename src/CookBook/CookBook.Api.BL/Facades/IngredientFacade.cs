using CookBook.Api.BL.Mappers;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class IngredientFacade(
    IIngredientRepository ingredientRepository,
    IngredientMapper mapper)
    : FacadeBase<IIngredientRepository, IngredientEntity>(ingredientRepository),
    IIngredientFacade
{
    private readonly IIngredientRepository ingredientRepository = ingredientRepository;
    private readonly IngredientMapper mapper = mapper;

    public List<IngredientListModel> GetAll()
    {
        return mapper.ToListModels(ingredientRepository.GetAll());
    }

    public IngredientDetailModel? GetById(Guid id)
    {
        var ingredientEntity = ingredientRepository.GetById(id);
        return ingredientEntity == null
            ? null
            : mapper.ToDetailModel(ingredientEntity);
    }

    public Guid CreateOrUpdate(IngredientDetailModel ingredientModel, string? ownerId = null)
    {
        return ingredientRepository.Exists(ingredientModel.Id)
            ? Update(ingredientModel, ownerId)!.Value
            : Create(ingredientModel, ownerId);
    }

    public Guid Create(IngredientDetailModel ingredientModel, string? ownerId = null)
    {
        var ingredientEntity = mapper.ToEntity(ingredientModel, ownerId);
        return ingredientRepository.Insert(ingredientEntity);
    }

    public Guid? Update(IngredientDetailModel ingredientModel, string? ownerId = null)
    {
        ThrowIfWrongOwner(ingredientModel?.Id, ownerId);

        var ingredientEntity = mapper.ToEntity(ingredientModel, ownerId);
        return ingredientRepository.Update(ingredientEntity);
    }

    public void Delete(Guid id, string? ownerId = null)
    {
        ThrowIfWrongOwner(id, ownerId);

        ingredientRepository.Remove(id);
    }
}
