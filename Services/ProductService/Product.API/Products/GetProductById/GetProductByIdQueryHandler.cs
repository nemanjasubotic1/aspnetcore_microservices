using FluentValidation.Results;
using GeneralUsing.CQRS;
using Services.ProductService.ProductCategory.API.Data;

namespace Services.ProductService.ProductCategory.API.Categories.GetCategoryById;

public record GetProductByIdQuery(Guid Id) : IQuery<CustomApiResponse>;


public class GetProductByIdQueryHandler(IProductRepository productRepository) : IQueryHandler<GetProductByIdQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productFromDb = await productRepository.GetProductByIdWithCategory(request.Id, cancellationToken);

        if (productFromDb == null)
        {
            return new CustomApiResponse(null, false, [new ValidationFailure()]);
        }

        return new CustomApiResponse(productFromDb);

    }
}
