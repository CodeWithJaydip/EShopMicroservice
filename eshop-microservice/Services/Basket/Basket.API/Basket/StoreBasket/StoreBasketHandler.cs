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
    internal class StoreBasketCommandHandler(IBasketRepository repository, Discount.gRPC.Discount.DiscountClient discountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>


    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Cart.Items)
            {
                var discount = await discountClient.GetDiscountAsync(new Discount.gRPC.GetDiscountRequest { ProductName = item.ProductName });
                item.Price -= (decimal)discount.Amount;
            }
            // Store the basket in the repository
            await repository.StoreBasketAsync(request.Cart);
            return new StoreBasketResult(request.Cart.UserName);
        }
    }
}
