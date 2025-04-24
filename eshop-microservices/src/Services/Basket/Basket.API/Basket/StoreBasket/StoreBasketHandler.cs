


using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;
    public record StoreBasketResult(string userName);
    public class StoreBasketCommandValidator:AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x=>x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UseName).NotEmpty().WithMessage("Username is required");
        }
    }
    public class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var cart = command.Cart;
            //TODO: store basket in database if not exist else update basket in database
            //TODO: update cache
            await repository.StoreBasket(cart,cancellationToken);
            return new StoreBasketResult(command.Cart.UseName);
        }
    }
}
