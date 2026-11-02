using CookBook.Common.Models.Resources.Texts;
using FluentValidation;

namespace CookBook.Common.Models;

public record IngredientDetailModel : IWithId
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? ImageUrl { get; set; }
}

public class IngredientDetailModelValidator : AbstractValidator<IngredientDetailModel>
{
    public IngredientDetailModelValidator()
    {
        const int nameMinimumLength = 3;
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(IngredientDetailModelResources.Name_Required_ErrorMessage)
            .MinimumLength(nameMinimumLength)
            .WithMessage(string.Format(IngredientDetailModelResources.Name_MinimumLength_ErrorMessage, nameMinimumLength));

        const int descriptionMinimumLength = 10;
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(IngredientDetailModelResources.Description_Required_ErrorMessage)
            .MinimumLength(descriptionMinimumLength)
            .WithMessage(string.Format(IngredientDetailModelResources.Description_MinimumLength_ErrorMessage, descriptionMinimumLength));
    }
}
