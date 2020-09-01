using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class Cart
    {
        public string UserName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public Cart()
        {
            
        }
        public Cart(string userName)
        {
            this.UserName = userName;
        }

        //calculate total price of items in cart
        public decimal TotalPrice {
            get 
            {
                decimal price = 0;
                foreach (var item in Items)
                {
                    price += item.Price * item.Quantity;
                }
                return price;
            }
        }

    }
}
