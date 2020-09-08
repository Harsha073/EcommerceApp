using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.ApiCollection.Interfaces;
using WebApp.Models;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketApi _basketApi;

        public IndexModel(ICatalogApi catalogApi, IBasketApi basketApi)
        {
            _catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
            _basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
        }

        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogApi.GetCatalog();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _catalogApi.GetCatalog(productId);

            var userName = "Harsha";
            var basket = await _basketApi.GetBasket(userName);

            basket.Items.Add(new CartItem()
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                Color = "Black"
            });

            var updatedBasket = await _basketApi.UpdateBasket(basket);             
            return RedirectToPage("Cart");
        }
    }
}
