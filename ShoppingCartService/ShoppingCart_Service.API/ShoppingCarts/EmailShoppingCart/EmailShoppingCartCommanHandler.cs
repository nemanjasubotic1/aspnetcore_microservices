using GeneralUsing.CQRS;
using GeneralUsing.Exceptions.CustomExceptionHandlers;
using Integration.AzureServiceBusSender;
using ShoppingCart_Service.API.Data;
using ShoppingCart_Service.API.Models.DTOs;

namespace ShoppingCart_Service.API.ShoppingCarts.EmailShoppingCart;

public record EmailShoppingCartCommand(ShoppingCartDTO ShoppingCartDTO) : ICommand<CustomApiResponse>;
//public record EmailShoppingCartResult(bool IsSucces);


public class EmailShoppingCartCommanHandler(IShoppingCartRepository shoppingCartRepository, IMessageService messageService
    , IConfiguration configuration) : ICommandHandler<EmailShoppingCartCommand, CustomApiResponse>
{
    string topicName = configuration["TopicsAndQueueNames:ShoppingCartTopic"]!;

    public async Task<CustomApiResponse> Handle(EmailShoppingCartCommand command, CancellationToken cancellationToken)
    {
        await messageService.PublishMessage(command.ShoppingCartDTO, topicName);

        return new CustomApiResponse();
    }
}
