using Basket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<Cart> GetBasket(string userName);

        Task<Cart> UpdateBasketItems(Cart cart);

        Task<bool> DeleteBasketById(string userName);
    }
}
