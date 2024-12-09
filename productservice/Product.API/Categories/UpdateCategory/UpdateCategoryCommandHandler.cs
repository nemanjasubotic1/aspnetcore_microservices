using FluentValidation;
using GeneralUsing.CQRS;
using GeneralUsing.Exceptions.CustomExceptionHandlers;
using Mapster;
using ProductCategory.API.Data;
using ProductCategory.API.Models;

namespace ProductCategory.API.Categories.UpdateCategory;


public record UpdateCategoryCommand(Guid Id, string Name, string Description) : ICommand<UpdateCategoryResult>;
public record UpdateCategoryResult(Guid Id);

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
{
    public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryFromDb = await categoryRepository.GetAsync(request.Id, cancellationToken);

        if (categoryFromDb == null)
        {
            throw new NotFoundException($"Entity with id {request.Id} dont exist.");
        }

        var category = request.Adapt<Category>();

        await categoryRepository.Update(category);

        return new UpdateCategoryResult(request.Id);
    }
}
