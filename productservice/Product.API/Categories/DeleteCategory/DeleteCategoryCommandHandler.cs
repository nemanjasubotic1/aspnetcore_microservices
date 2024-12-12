﻿using GeneralUsing.CQRS;
using ProductCategory.API.Data;

namespace ProductCategory.API.Categories.DeleteCategory;

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
