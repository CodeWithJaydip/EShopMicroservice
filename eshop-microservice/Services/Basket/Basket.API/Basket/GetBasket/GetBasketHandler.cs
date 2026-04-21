namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string userName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    internal class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            // get basket from repository
                var basket = await repository.GetBasketAsync(request.userName);
                if (basket is not null)
                {
                    return new GetBasketResult(basket);
                }
            // if basket is null, return an empty basket

            return new GetBasketResult(new ShoppingCart(request.userName));
        }
    }
}
