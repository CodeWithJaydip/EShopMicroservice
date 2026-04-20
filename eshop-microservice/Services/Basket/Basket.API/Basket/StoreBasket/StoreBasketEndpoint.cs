

using Mapster;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/baskets", async (StoreBasketRequest request, ISender sender) =>
            {
                var result = await sender.Send(new StoreBasketCommand(request.Cart));
                var response = result.Adapt<StoreBasketResponse>();
                return Results.Ok(response);
            }).WithName("StoreBasket").WithTags("Baskets");         
        }
    }
}
