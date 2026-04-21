namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName);
        Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket);
        Task<bool> DeleteBasketAsync(string userName);
    }
}
