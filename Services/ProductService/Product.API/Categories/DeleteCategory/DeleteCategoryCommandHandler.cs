using GeneralUsing.CQRS;

namespace Main.ProductService.ProductCategory.API.InitialData;

public record DeleteCategoryCommand(Guid Id) : ICommand<CustomApiResponse>;
//public record DeleteCategoryResult(bool IsSuccess);


public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<DeleteCategoryCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await categoryRepository.RemoveAsync(request.Id, cancellationToken);

        return new CustomApiResponse(IsSuccess: true);
    }
}
