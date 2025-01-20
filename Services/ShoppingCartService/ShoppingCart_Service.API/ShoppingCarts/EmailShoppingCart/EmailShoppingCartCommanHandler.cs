using GeneralUsing.CQRS;
using Integration.AzureServiceBusSender;
using Main.ShoppingCartService.ShoppingCart_Service.API.Data;
using Main.ShoppingCartService.ShoppingCart_Service.API.Models.DTOs;
using ShoppingCart_Service.API.Models.DTOs;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.EmailShoppingCart;

public record EmailShoppingCartCommand(EmailCartDTO EmailCartDTO) : ICommand<CustomApiResponse>;
//public record EmailShoppingCartResult(bool IsSucces);


public class EmailShoppingCartCommanHandler(IShoppingCartRepository shoppingCartRepository, IMessageService messageService
    , IConfiguration configuration) : ICommandHandler<EmailShoppingCartCommand, CustomApiResponse>
{
    string topicName = configuration["TopicsAndQueueNames:ShoppingCartTopic"]!;

    string connectionStrings = configuration["AzureBusService:ConnectionString"]!;

    public async Task<CustomApiResponse> Handle(EmailShoppingCartCommand command, CancellationToken cancellationToken)
    {
        await messageService.PublishMessage(command.EmailCartDTO, topicName, connectionStrings);

        return new CustomApiResponse();
    }
}
