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

    public Guid Create(IngredientDetailModel ingredientModel, string? ownerId)
    {
        var ingredientEntity = mapper.ToEntity(ingredientModel, ownerId);
        return ingredientRepository.Insert(ingredientEntity);
    }

    public Guid? Update(IngredientDetailModel ingredientModel)
    {
        var ingredientEntity = mapper.ToEntity(ingredientModel);
        return ingredientRepository.Update(ingredientEntity);
    }

    public void Delete(Guid id)
    {
        ingredientRepository.Remove(id);
    }
}
