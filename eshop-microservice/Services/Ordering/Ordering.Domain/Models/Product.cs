namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;

        public static Product Create(ProductId productId, string name, decimal price)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            var product = new Product
            {
                Id = productId,
                Name = name,
                Price = price
            };
            return product;
        }
    }
}
