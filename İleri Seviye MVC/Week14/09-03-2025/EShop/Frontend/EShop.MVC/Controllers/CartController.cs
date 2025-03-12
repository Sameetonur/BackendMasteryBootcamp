using EShop.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EShop.MVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IToastNotification _toastNotification;


        public CartController(ICartService cartService, IToastNotification toastNotification)
        {
            _cartService = cartService;
            _toastNotification = toastNotification;
        }

        // GET: CartController
        public async Task<IActionResult> Index()
        {
            var response = await _cartService.GetCartAsync();
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Error ?? "Sepet bilgisi alınamadı!");
                return RedirectToAction("Index", "Homde");

            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            if (quantity < 1)
            {
                _toastNotification.AddErrorToastMessage("Geçersiz ürün miktarı!");
                return RedirectToAction("Index", "Home");
            }
            var response = await _cartService.AddToCartAsync(productId, quantity);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public async Task<IActionResult> ChangeQuantity(int cartItemId, int quantity)
        {
            if (quantity < 1)
            {
                _toastNotification.AddErrorToastMessage("Geçersiz ürün miktarı!");
                return RedirectToAction(nameof(Index));
            }
            var response = await _cartService.ChangeQuantityAsync(cartItemId, quantity);
            return RedirectToAction(nameof(Index));

        }

    }
}
