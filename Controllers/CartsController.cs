using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Services;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;

        public CartsController(ApplicationDbContext context, ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> MyCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            var cartViewModel = await _cartService.GetCartViewModelAsync(userId);
            return View(cartViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            if (!await _cartService.HasItemsAsync(userId))
            {
                return RedirectToAction(nameof(MyCart));
            }

            return View(new CheckoutViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            if (!await _cartService.HasItemsAsync(userId))
            {
                return RedirectToAction(nameof(MyCart));
            }

            await _cartService.ClearCartAsync(userId);

            TempData["SuccessMessage"] = "Uspješno kupljeno!";
            return RedirectToAction("Index", "Home");
        }
    }
}
