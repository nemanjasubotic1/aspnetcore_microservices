using FluentValidation.Results;
using GeneralUsing.CQRS;
using Mapster;
using OrderingService.Application.Data;
using OrderingService.Domain.Models;

namespace OrderingService.Application.Customers.Commands.UpdateCustomerCommand;

public class UpdateCustomerCommandHandler(IAppDbContext dbContext) : ICommandHandler<UpdateCustomerCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = request.CustomerDTO.Adapt<Customer>();

        dbContext.Customers.Update(customer);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return new CustomApiResponse(null, false, [new ValidationFailure("", "Something went wrong with update")]);
        }

        return new CustomApiResponse(null, true);
    }
}


