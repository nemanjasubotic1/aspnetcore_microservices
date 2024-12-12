using GeneralUsing.CQRS;
using ProductCategory.API.Data;

namespace ProductCategory.API.Categories.DeleteCategory;

public record DeleteProductCommand(Guid Id) : ICommand<CustomApiResponse>;
//public record DeleteProductResult(bool IsSuccess);


public class DeleteProductCommandHandler(IProductRepository productRepository) : ICommandHandler<DeleteProductCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await productRepository.RemoveAsync(request.Id, cancellationToken);

        return new CustomApiResponse();
    }
}
