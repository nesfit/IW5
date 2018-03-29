# Zadání

1. Opravte všechny padající testy
   1. ReSharper > Unit Tests > Run All Tests from Solution ( **CTRL + U, L** )
1. Změňte signaturu metody `RecipeRepository.FindByName`
   1. ```public RecipeDetailModel FindByName(string name)```
   1. Opravte metodu, tak aby vracela nový datový typ
   1. Opravte všechny padající testy
1. Naimplementujte metodu GetById
   1. `public RecipeDetailModel GetById(Guid id)`
   1. Napište testy na novou metodu
      1. V databázi najdete id `cb8db9b3-799c-4ef2-9d85-ce32a9ffa843`
1. Naimplementujte metodu GetAllRecipe
   1. `public IList<RecipeListModel> GetAllRecipe()`
   1. Napište testy na novou metodu
1. Vytvořte třídu `Mapper`
   1. Přesuňte sem veškeré mapování ze třídy `RecipeRepository`
   2. Použijte `Mapper` v `RecipeRepository`

**Pamatujte na cleancode!**

      