using FluentValidation;
using GeneralUsing.CQRS;
using Main.ShoppingCartService.ShoppingCart_Service.API.Data;
using Main.ShoppingCartService.ShoppingCart_Service.API.Models;
using Main.ShoppingCartService.ShoppingCart_Service.API.Models.DTOs;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.CreateUpdateShoppingCart;

public record CreateShoppingCartCommand(ShoppingCartDTO ShoppingCartDTO) : ICommand<CustomApiResponse>;
//public record CreateShoppingCartResult(Guid Id);

public class CreateShoppingCartCommandValidator : AbstractValidator<CreateShoppingCartCommand>
{
    public CreateShoppingCartCommandValidator()
    {
        RuleFor(l => l.ShoppingCartDTO.CartItems).NotEmpty().WithMessage("Cart Items are required");
    }
}

// TODO CartItem Validator

public class CreateShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository) : ICommandHandler<CreateShoppingCartCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var cartFromDb = await shoppingCartRepository.GetShoppingCartByUserIdAsync(request.ShoppingCartDTO.UserId, cancellationToken);

        // cart for this user dont exist, create new cart and populate it with items
        if (cartFromDb.UserId == null)
        {
            var cart = new ShoppingCart
            {
                UserId = request.ShoppingCartDTO.UserId,
                CartItems = request.ShoppingCartDTO.CartItems,
                Discount = request.ShoppingCartDTO.Discount,
            };

            await shoppingCartRepository.CreateShoppingCart(cart);

            return new CustomApiResponse(cart.Id);
        }
        // cart for this user exists, change items quantity or add new items
        else
        {
            foreach (var item in request.ShoppingCartDTO.CartItems)
            {
                var existingItem = cartFromDb.CartItems.FirstOrDefault(l => l.Id == item.Id);

                // item exists in cart, update quantity
                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                }
                // item dont exist in cart, add new item
                else
                {
                    cartFromDb.CartItems.Add(item);
                }
            }

            cartFromDb.CouponName = request.ShoppingCartDTO.CouponName;
            cartFromDb.Discount = request.ShoppingCartDTO.Discount;

            await shoppingCartRepository.UpdateShoppingCart(cartFromDb, cancellationToken);
        }

        return new CustomApiResponse(cartFromDb.Id);
    }
}
