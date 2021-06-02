using Shopping.Aggregrator.Extentions;
using Shopping.Aggregrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregrator.Services
{
    public class BasketService :IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/api/v1/Basket/{userName}");
            return await response.ReadContentAs<BasketModel>();
        }
    }
}
