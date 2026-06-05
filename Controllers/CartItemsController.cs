using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Services;


namespace WebShop.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ICartService _cartService;

        public CartItemsController(ApplicationDbContext context, ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult AddToCart()
        {
            return RedirectToAction("AllProducts", "Products");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartService.RemoveItemAsync(cartItemId);

            return RedirectToAction("MyCart", "Carts");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IncreaseQuantity(int cartItemId)
        {
            await _cartService.IncreaseQuantityAsync(cartItemId);

            return RedirectToAction("MyCart", "Carts");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DecreaseQuantity(int cartItemId)
        {
            await _cartService.DecreaseQuantityAsync(cartItemId);

            return RedirectToAction("MyCart", "Carts");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, string? returnUrl = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            await _cartService.AddToCartAsync(userId, productId);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("AllProducts", "Products");
        }
    }
}
