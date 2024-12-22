using FluentValidation;
using GeneralUsing.CQRS;
using GeneralUsing.Exceptions.CustomExceptionHandlers;
using Mapster;
using Services.ProductService.ProductCategory.API.Data;
using Services.ProductService.ProductCategory.API.Models;
using Services.ProductService.ProductCategory.API.Models.DTOs;

namespace Services.ProductService.ProductCategory.API.Categories.UpdateCategory;


public record UpdateProductCommand(ProductDTO ProductDTO) : ICommand<CustomApiResponse>;
//public record UpdateProductResult(Guid Id);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductDTO.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.ProductDTO.Name).NotEmpty().WithMessage("Name is required");
    }
}

public class UpdateProductCommandHandler(IProductRepository productRepository) : ICommandHandler<UpdateProductCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productFromDb = await productRepository.GetAsync(request.ProductDTO.Id, cancellationToken);

        if (productFromDb == null)
        {
            throw new NotFoundException($"Entity with id {request.ProductDTO.Id} dont exist.");
        }

        var product = request.ProductDTO.Adapt<Product>();

        await productRepository.Update(product);

        return new CustomApiResponse(request.ProductDTO.Id);
    }
}
