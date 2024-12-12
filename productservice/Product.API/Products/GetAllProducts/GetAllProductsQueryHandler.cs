using GeneralUsing.CQRS;
using ProductCategory.API.Data;
using ProductCategory.API.Models;

namespace ProductCategory.API.Products.GetAllProducts;

public record GetAllProductsQuery(int? pageNumber = 1, int? pageSize = 1) : IQuery<CustomApiResponse>;
//public record GetAllProductsResult(IEnumerable<Product> Products);


public class GetAllProductsQueryHandler(IProductRepository productRepository) : IQueryHandler<GetAllProductsQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var listOfProducts = await productRepository.GetAllProductsWithCategory(cancellationToken, request.pageNumber, request.pageSize);

        return new CustomApiResponse(listOfProducts);
    }
}
