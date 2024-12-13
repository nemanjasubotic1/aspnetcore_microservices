using FluentValidation.Results;
using GeneralUsing.CQRS;
using Marten.Linq.QueryHandlers;
using ProductCategory.API.Data;

namespace ProductCategory.API.Categories.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IQuery<CustomApiResponse>;


public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<GetCategoryByIdQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryFromDb = await categoryRepository.GetAsync(request.Id, cancellationToken);

        if (categoryFromDb == null)
        {
            return new CustomApiResponse(null, false, [new ValidationFailure()]);
        }

        return new CustomApiResponse(categoryFromDb);

    }
}
