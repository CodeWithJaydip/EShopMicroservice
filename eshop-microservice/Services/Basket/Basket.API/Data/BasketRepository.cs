

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync();
            return true;
        }

        public Task<ShoppingCart> GetBasketAsync(string userName)
        {
            var basket = session.LoadAsync<ShoppingCart>(userName);
            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket)
        {
            session.Store(basket);
            await session.SaveChangesAsync();
            return basket;
        }
    }
}
