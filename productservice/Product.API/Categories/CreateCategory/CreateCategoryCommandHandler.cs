using FluentValidation;
using GeneralUsing.CQRS;
using ProductCategory.API.Data;
using ProductCategory.API.Models;

namespace ProductCategory.API.Categories.CreateCategory;

public record CreateCategoryCommand(string Name, string Description) : ICommand<CreateCategoryResult>;
public record CreateCategoryResult(Guid Id);

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(l => l.Name).NotEmpty().WithMessage("The name is required");
    }
}

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
{
    public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = command.Name,
            Description = command.Description,
        };

        await categoryRepository.CreateAsync(category);

        return new CreateCategoryResult(category.Id);
    }
}
