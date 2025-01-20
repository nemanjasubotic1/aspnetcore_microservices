using GeneralUsing.CQRS;

namespace Main.ProductService.ProductCategory.API.InitialData;

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
