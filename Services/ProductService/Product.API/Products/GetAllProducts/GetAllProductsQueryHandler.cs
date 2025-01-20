using GeneralUsing.CQRS;

namespace Main.ProductService.ProductCategory.API.InitialData;

public record GetAllProductsQuery(int? pageNumber = 1, int? pageSize = 10) : IQuery<CustomApiResponse>;
//public record GetAllProductsResult(IEnumerable<Product> Products);


public class GetAllProductsQueryHandler(IProductRepository productRepository) : IQueryHandler<GetAllProductsQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var listOfProducts = await productRepository.GetAllProductsWithCategory(cancellationToken, request.pageNumber, request.pageSize);

        return new CustomApiResponse(listOfProducts);
    }
}
