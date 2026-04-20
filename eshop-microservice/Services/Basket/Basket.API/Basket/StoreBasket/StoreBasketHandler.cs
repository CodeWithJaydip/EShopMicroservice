
namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart should not be empty.");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required.");
        }
    }
    internal class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            // Store the basket in the repository
            // For example: await _repository.SaveBasketAsync(request.Cart);
            return new StoreBasketResult("pqr");
        }
    }
}
