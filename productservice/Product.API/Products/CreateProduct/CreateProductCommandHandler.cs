using GeneralUsing.CQRS;
using Marten;
using ProductCategory.API.Data;
using ProductCategory.API.Models;
using ProductCategory.API.Models.DTOs;
using System.Linq.Expressions;

namespace ProductCategory.API.Products.CreateProduct;

public record CreateProductCommand(ProductDTO ProductDTO) : ICommand<CustomApiResponse>;
//public record CreateProductResult(Guid Id);

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
