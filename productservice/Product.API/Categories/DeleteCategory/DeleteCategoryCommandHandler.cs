using GeneralUsing.CQRS;
using ProductCategory.API.Data;

namespace ProductCategory.API.Categories.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : ICommand<DeleteCategoryResult>;
public record DeleteCategoryResult(bool IsSuccess);


public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
{
    public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await categoryRepository.RemoveAsync(request.Id, cancellationToken);

        return new DeleteCategoryResult(true);
    }
}
