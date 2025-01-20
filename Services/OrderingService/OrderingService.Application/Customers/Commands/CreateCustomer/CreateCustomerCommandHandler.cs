using GeneralUsing.CQRS;
using Services.OrderingService.OrderingService.Application.Data;
using Services.OrderingService.OrderingService.Domain.Models;

namespace Services.OrderingService.OrderingService.Application.Customers.Commands.CreateCustomer;
public class CreateCustomerCommandHandler(IAppDbContext dbContext) : ICommandHandler<CreateCustomerCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = command.CustomerDTO.Id,
            Username = command.CustomerDTO.Username,
            Name = command.CustomerDTO.Name,
            //LastName = command.CustomerDTO.LastName,
            EmailAddress = command.CustomerDTO.EmailAddress,
        };

        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CustomApiResponse(Result: customer);

    }
}
