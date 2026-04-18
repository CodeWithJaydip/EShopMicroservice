using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProductById
{
    //public record GetProductByIdRequest(Guid Id)
    public record GetProductByIdResponse(Product product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender send) =>
            {
                
                var query = new GetProductByIdQuery(id);
                var result = await send.Send(query);
                var response = new GetProductByIdResponse(result.product);
                return Results.Ok(response);
            })
             .WithName("GetProductById")
             .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Product By Id")
             .WithDescription("Get Product By Id");
        }
    }
}
