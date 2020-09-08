using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.ApiCollection.Interfaces;
using WebApp.Models;

namespace WebApp
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketApi _basketApi;

        public CheckOutModel(IBasketApi basketApi)
        {
            _basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
        }

        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "Harsha";
            Cart = await _basketApi.GetBasket(userName);
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var userName = "Harsha";
            Cart = await _basketApi.GetBasket(userName);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = "Harsha";
            Order.TotalPrice = Cart.TotalPrice;

            await _basketApi.CheckoutBasket(Order);

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}