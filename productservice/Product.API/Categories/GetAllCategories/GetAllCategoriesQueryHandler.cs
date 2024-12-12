using GeneralUsing.CQRS;
using Marten;
using ProductCategory.API.Data;
using ProductCategory.API.Models;

namespace ProductCategory.API.Categories.GetAllCategories;

public record GetAllCategoriesQuery(int? pageNumber = 1, int? pageSize = 1) : IQuery<CustomApiResponse>;
//public record GetAllCategoriesResult(IEnumerable<Category> Categories);

public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IDocumentSession session) : IQueryHandler<GetAllCategoriesQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var listOfCategories = await categoryRepository.GetAllCategoriesWithProducts(cancellationToken, request.pageNumber, request.pageSize);

        return new CustomApiResponse(listOfCategories, true);
    }
}
