using Mapster;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsSuccess);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/baskets/{username}", async (string username, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(username));
                var response = result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            }).WithName("DeleteBasket").WithTags("Baskets");
        }
    {
    }
}
