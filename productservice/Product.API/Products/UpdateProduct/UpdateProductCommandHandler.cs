using FluentValidation;
using GeneralUsing.CQRS;
using GeneralUsing.Exceptions.CustomExceptionHandlers;
using Mapster;
using ProductCategory.API.Data;
using ProductCategory.API.Models;
using ProductCategory.API.Models.DTOs;

namespace ProductCategory.API.Categories.UpdateCategory;


public record UpdateProductCommand(ProductDTO ProductDTO) : ICommand<UpdateProductResult>;
public record UpdateProductResult(Guid Id);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductDTO.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.ProductDTO.Name).NotEmpty().WithMessage("Name is required");
    }
}

public class UpdateProductCommandHandler(IProductRepository productRepository) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productFromDb = await productRepository.GetAsync(request.ProductDTO.Id, cancellationToken);

        if (productFromDb == null)
        {
            throw new NotFoundException($"Entity with id {request.ProductDTO.Id} dont exist.");
        }

        var product = request.ProductDTO.Adapt<Product>();

        await productRepository.Update(product);

        return new UpdateProductResult(request.ProductDTO.Id);
    }
}
