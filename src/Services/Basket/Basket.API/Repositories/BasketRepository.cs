using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _rediscache;

        public BasketRepository(IDistributedCache redis) {
            _rediscache = redis ?? throw new ArgumentNullException(nameof(redis));
        }
        public async Task DeleteBasket(string userName)
        {
            await _rediscache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var bucket = await _rediscache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(bucket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(bucket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _rediscache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
