using FluentValidation;
using GeneralUsing.CQRS;
using ProductCategory.API.Data;
using ProductCategory.API.Models;
using ProductCategory.API.Models.DTOs;

namespace ProductCategory.API.Products.CreateProduct;

public record CreateProductCommand(ProductDTO ProductDTO) : ICommand<CustomApiResponse>;
//public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(l => l.ProductDTO.Name).NotEmpty().WithMessage("The Category name is requied");
    }
}

public class CreateProductCommandHandler(IProductRepository productRepository) : ICommandHandler<CreateProductCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.ProductDTO.Name,
            Description = request.ProductDTO.Description,
            Price = request.ProductDTO.Price,
            ImageUrl = request.ProductDTO.ImageUrl,
            YearOfProduction = request.ProductDTO.YearOfProduction,
            CategoryId = request.ProductDTO.CategoryId,
        };

        await productRepository.CreateAsync(product);

        return new CustomApiResponse(product.Id);

    }
}
