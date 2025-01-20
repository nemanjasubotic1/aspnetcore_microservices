using FluentValidation;
using GeneralUsing.CQRS;
using GeneralUsing.Exceptions.CustomExceptionHandlers;
using Mapster;

namespace Main.ProductService.ProductCategory.API.InitialData;


public record UpdateCategoryCommand(CategoryDTO CategoryDTO) : ICommand<CustomApiResponse>;
//public record UpdateCategoryResult(Guid Id);

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.CategoryDTO.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.CategoryDTO.Name).NotEmpty().WithMessage("Name is required");
    }
}

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<UpdateCategoryCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryFromDb = await categoryRepository.GetAsync(request.CategoryDTO.Id, cancellationToken);

        if (categoryFromDb == null)
        {
            throw new NotFoundException($"Entity with id {request.CategoryDTO.Id} dont exist.");
        }

        var category = request.CategoryDTO.Adapt<Category>();

        await categoryRepository.Update(category);

        return new CustomApiResponse(request.CategoryDTO.Id);
    }
}
