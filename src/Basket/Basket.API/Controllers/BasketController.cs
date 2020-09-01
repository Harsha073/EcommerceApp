using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NotFound)]
        public async Task<ActionResult<Cart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);

            return Ok(basket ?? new Cart(userName));
        }

        [HttpPost]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> UpdateBasket([FromBody]Cart cart)
        {
            var basket = await _repository.UpdateBasketItems(cart);
            return Ok(basket);
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            return Ok(await _repository.DeleteBasketById(userName));
        }
    }
}
