using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.ApiCollection.Interfaces;
using WebApp.Models;

namespace WebApp
{
    public class CartModel : PageModel
    {
        private readonly IBasketApi _basketApi;

        public CartModel(IBasketApi basketApi)
        {
            _basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
        }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "Harsha";
            Cart = await _basketApi.GetBasket(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var userName = "Harsha";
            var basket = await _basketApi.GetBasket(userName);

            int index = basket.Items.FindIndex(item => item.ProductId == productId);
            basket.Items.RemoveAt(index);

            var updatedBasket = await _basketApi.UpdateBasket(basket);
            return RedirectToPage();
        }
    }
}