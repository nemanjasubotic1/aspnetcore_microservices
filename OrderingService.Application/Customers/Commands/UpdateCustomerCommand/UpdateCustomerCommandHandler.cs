using GeneralUsing.CQRS;
using Mapster;
using OrderingService.Application.Data;
using OrderingService.Domain.Models;

namespace OrderingService.Application.Customers.Commands.UpdateCustomerCommand;

public class UpdateCustomerCommandHandler(IAppDbContext dbContext) : ICommandHandler<UpdateCustomerCommand, UpdateCustomerCommandResult>
{
    public async Task<UpdateCustomerCommandResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = request.CustomerDTO.Adapt<Customer>();

        dbContext.Customers.Update(customer);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return new UpdateCustomerCommandResult(false);
        }

        return new UpdateCustomerCommandResult(true);
    }
}


