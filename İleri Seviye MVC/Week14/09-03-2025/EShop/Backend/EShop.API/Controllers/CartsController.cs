using EShop.Services.Abstract;
using EShop.Shared.ControllerBases;
using EShop.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController : CustomControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCart()//   /carts
        {
            var response = await _cartService.GetCartAsync(GetUserId());
            return CreateResult(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto  addToCartDto)
        {
            addToCartDto.ApplicationUserId=GetUserId();
            var response = await _cartService.AddToCartAsync(addToCartDto);
            return CreateResult(response);
        }

        [Authorize]
        [HttpDelete("{cartItemId}")] //  /carts
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var response = await _cartService.RemoveFromCartAsync(cartItemId);
            return CreateResult(response);
        }

        [Authorize]
        [HttpPut("{cartItemId}/quantity")] // /carts/12/quantity?quantity=45
        public async Task<IActionResult> ChangeQuantity(int cartItemId, [FromQuery]  int quantity)
        {
            ChangeQuantityDto  changeQuantityDto = new(){CartItemId=cartItemId,Quantity=quantity};
            var response = await _cartService.ChangeQuantityAsync(changeQuantityDto);
            return CreateResult(response);
        }

        [Authorize]
        [HttpDelete("items")] // /carts/items
        public async Task<IActionResult> Clear()
        {
            var response = await _cartService.ClearCartAsync(GetUserId());
            return CreateResult(response);
        }
    }
}
