using FluentValidation;
using GeneralUsing.CQRS;

namespace Main.ProductService.ProductCategory.API.InitialData;

public record CreateCategoryCommand(CategoryDTO CategoryDTO) : ICommand<CustomApiResponse>;
//public record CreateCategoryResult(Guid Id, List<ValidationFailure>? Errors = null);

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(l => l.CategoryDTO.Name).NotEmpty().WithMessage("The name is required");
    }
}

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<CreateCategoryCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = command.CategoryDTO.Name,
            Description = command.CategoryDTO.Description,
        };

        await categoryRepository.CreateAsync(category);

        return new CustomApiResponse(category.Id);
    }
}
