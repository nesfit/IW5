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

    public Guid CreateOrUpdate(IngredientDetailModel ingredientModel, IList<string> userRoles, string? ownerId = null)
    {
        return ingredientRepository.Exists(ingredientModel.Id)
            ? Update(ingredientModel, userRoles, ownerId)!.Value
            : Create(ingredientModel, userRoles, ownerId);
    }

    public Guid Create(IngredientDetailModel ingredientModel, IList<string> userRoles, string? ownerId = null)
    {
        var ingredientEntity = mapper.ToEntity(ingredientModel, ownerId);
        return ingredientRepository.Insert(ingredientEntity);
    }

    public Guid? Update(IngredientDetailModel ingredientModel, IList<string> userRoles, string? ownerId = null)
    {
        ThrowIfWrongOwnerAndNotAdmin(ingredientModel.Id, userRoles, ownerId);

        var ingredientEntity = mapper.ToEntity(ingredientModel, ownerId);
        return ingredientRepository.Update(ingredientEntity);
    }

    public void Delete(Guid id, IList<string> userRoles, string? ownerId = null)
    {
        ThrowIfWrongOwnerAndNotAdmin(id, userRoles, ownerId);

        ingredientRepository.Remove(id);
    }
}
