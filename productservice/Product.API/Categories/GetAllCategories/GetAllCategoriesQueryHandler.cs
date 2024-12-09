using GeneralUsing.CQRS;
using Marten;
using Marten.Linq.QueryHandlers;
using Marten.Schema;
using ProductCategory.API.Data;
using ProductCategory.API.Models;
using System.Net;

namespace ProductCategory.API.Categories.GetAllCategories;

public record GetAllCategoriesQuery(int? pageNumber = 1, int? pageSize = 1) : IQuery<GetAllCategoriesResult>;
public record GetAllCategoriesResult(IEnumerable<Category> Categories);

public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IDocumentSession session) : IQueryHandler<GetAllCategoriesQuery, GetAllCategoriesResult>
{
    public async Task<GetAllCategoriesResult> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {

        var listOfCategories = await categoryRepository.GetAllCategoriesWithProduts(cancellationToken, request.pageNumber, request.pageSize);

        return new GetAllCategoriesResult(listOfCategories);
    }
}
