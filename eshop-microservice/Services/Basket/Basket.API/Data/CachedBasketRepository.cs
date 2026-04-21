
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName)
        {
            var isDeleted = await repository.DeleteBasketAsync(userName);
            if (isDeleted)
            {
                await cache.RemoveAsync(userName);
            }
            return isDeleted;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName)
        {
            var cachedBasket = await cache.GetStringAsync(userName);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            var basket = await repository.GetBasketAsync(userName);
            if (basket != null)
            {
                await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket));
            }
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket)
        {
            var storedBasket = await repository.StoreBasketAsync(basket);
            await cache.SetStringAsync(storedBasket.UserName, JsonSerializer.Serialize(storedBasket));
            return storedBasket;
        }
    }
}
