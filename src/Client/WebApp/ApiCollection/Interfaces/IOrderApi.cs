using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ApiCollection.Interfaces
{
    public interface IOrderApi
    {
        Task<IEnumerable<OrderModel>> GetOrdersByUserName(string userName);
    }
}
