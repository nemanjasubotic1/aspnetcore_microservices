using GeneralUsing.CQRS;
namespace Main.ProductService.ProductCategory.API.InitialData;

public record GetAllCategoriesQuery(int? pageNumber = 1, int? pageSize = 1) : IQuery<CustomApiResponse>;
//public record GetAllCategoriesResult(IEnumerable<Category> Categories);

public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<GetAllCategoriesQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var listOfCategories = await categoryRepository.GetAllCategoriesWithProducts(cancellationToken, request.pageNumber, request.pageSize);

        return new CustomApiResponse(listOfCategories);
    }
}
