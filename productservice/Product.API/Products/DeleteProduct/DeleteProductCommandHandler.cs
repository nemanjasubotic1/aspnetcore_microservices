using GeneralUsing.CQRS;
using ProductCategory.API.Data;

namespace ProductCategory.API.Categories.DeleteCategory;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);


public class DeleteProductCommandHandler(IProductRepository productRepository) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await productRepository.RemoveAsync(request.Id, cancellationToken);

        return new DeleteProductResult(true);
    }
}
