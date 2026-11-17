using CookBook.Api.DAL.EF;
using CookBook.Api.DAL.EF.Migrations;
using CookBook.Api.DAL.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Xunit;

namespace CookBook.API.DAL.IntegrationTests;

public class SeedSampleDataMigrationTests
{
    [Fact]
    public void Migration_seeds_the_same_records_as_memory_storage()
    {
        var storage = new Storage();
        var operations = new SeedSampleData().UpOperations.Cast<InsertDataOperation>().ToArray();

        Assert.Equal(new[] { "Ingredients", "Recipes", "IngredientAmounts" }, operations.Select(operation => operation.Table));
        AssertRows(operations[0], storage.Ingredients.Select(i => new object?[] { i.Id, i.Name, i.Description, i.ImageUrl }));
        AssertRows(operations[1], storage.Recipes.Select(r => new object?[] { r.Id, r.Name, r.Description, r.Duration, (int)r.FoodType, r.ImageUrl }));
        AssertRows(operations[2], storage.IngredientAmounts.Select(a => new object?[] { a.Id, a.Amount, (int)a.Unit, a.RecipeId, a.IngredientId }));
    }

    [Fact]
    public void Migration_rollback_deletes_only_seeded_ids_in_dependency_order()
    {
        var migration = new SeedSampleData();
        var inserts = migration.UpOperations.Cast<InsertDataOperation>().ToArray();
        var deletes = migration.DownOperations.Cast<DeleteDataOperation>().ToArray();

        Assert.Equal(new[] { "IngredientAmounts", "Recipes", "Ingredients" }, deletes.Select(operation => operation.Table));
        foreach (var delete in deletes)
        {
            var insert = Assert.Single(inserts, operation => operation.Table == delete.Table);
            Assert.Equal(Enumerable.Range(0, insert.Values.GetLength(0)).Select(i => insert.Values[i, 0]).OrderBy(id => id),
                Enumerable.Range(0, delete.KeyValues.GetLength(0)).Select(i => delete.KeyValues[i, 0]).OrderBy(id => id));
        }
    }

    [Fact]
    public void Migration_generates_sql_in_both_directions()
    {
        var options = new DbContextOptionsBuilder<CookBookDbContext>()
            .UseSqlServer("Server=localhost;Database=CookBook;User Id=test;Password=test;TrustServerCertificate=True")
            .Options;
        using var context = new CookBookDbContext(options);
        var migrator = context.GetService<IMigrator>();

        var up = migrator.GenerateScript("20261116094939_AddOwnerId", "20261116130000_SeedSampleData");
        var down = migrator.GenerateScript("20261116130000_SeedSampleData", "20261116094939_AddOwnerId");

        Assert.Contains("INSERT INTO [Ingredients]", up);
        Assert.Contains("INSERT INTO [Recipes]", up);
        Assert.Contains("INSERT INTO [IngredientAmounts]", up);
        Assert.Contains("DELETE FROM [IngredientAmounts]", down);
        Assert.Contains("DELETE FROM [Recipes]", down);
        Assert.Contains("DELETE FROM [Ingredients]", down);
    }

    private static void AssertRows(InsertDataOperation operation, IEnumerable<object?[]> expected)
    {
        var rows = expected.ToArray();
        Assert.Equal(rows.Length, operation.Values.GetLength(0));
        for (var row = 0; row < rows.Length; row++)
        {
            Assert.Equal(rows[row].Length, operation.Values.GetLength(1));
            for (var column = 0; column < rows[row].Length; column++)
            {
                Assert.Equal(rows[row][column], operation.Values[row, column]);
            }
        }
    }
}
