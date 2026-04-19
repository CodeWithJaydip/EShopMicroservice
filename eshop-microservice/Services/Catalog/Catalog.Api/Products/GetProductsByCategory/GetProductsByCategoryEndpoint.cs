using Catalog.Api.Products.GetProducts;

namespace Catalog.Api.Products.GetProductsByCategory
{
    public record GetProductsByCategoryRequest(string category);
    public record GetProductsByCategoryResponse(IEnumerable<Product> products);
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/productsByCategory/{category}", async (string category, ISender send) =>
            {
                var query = new GetProductsByCategoryQuery(category);
                var result = await send.Send(query);
                var response = result.Adapt<GetProductsByCategoryResponse>();
                return Results.Ok(response);
            })
         .WithName("GetProductsByCategory")
         .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Products by Category")
         .WithDescription("Get Products by Category");
        }
    }
}
