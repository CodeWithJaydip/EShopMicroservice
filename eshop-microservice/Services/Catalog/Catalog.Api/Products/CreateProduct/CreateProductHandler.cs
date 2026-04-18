
namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories,string Description, decimal Price, string ImageFile): ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Create Domain Entity

            Product product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Categories = request.Categories,
                Description = request.Description,
                Price = request.Price,
                ImageFile = request.ImageFile
            };

            // Save to Database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return Result
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
