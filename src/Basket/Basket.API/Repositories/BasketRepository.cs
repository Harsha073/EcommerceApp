using Basket.API.Data.Interfaces;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext basketContext)
        {
            this._context = basketContext;
        }

        public async Task<Cart> GetBasket(string userName)
        {
            var basket = await _context
                                    .Redis
                                    .StringGetAsync(userName);

            if (basket.IsNullOrEmpty)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Cart>(basket);
        }

        public async Task<Cart> UpdateBasketItems(Cart cart)
        {
            var value = JsonConvert.SerializeObject(cart);

            var updated = await _context
                                    .Redis
                                    .StringSetAsync(cart.UserName, value);

            if (!updated)
            {
                return null;
            }

            return await GetBasket(cart.UserName);
        }

        public async Task<bool> DeleteBasketById(string userName)
        {
            return await _context
                                .Redis
                                .KeyDeleteAsync(userName);
        }

    }
}
