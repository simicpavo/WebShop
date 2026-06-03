using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.ViewModels;

namespace WebShop.Services;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CartViewModel> GetCartViewModelAsync(string userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .ThenInclude(p => p.Discount)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || cart.CartItems.Count == 0)
        {
            return new CartViewModel();
        }

        var items = cart.CartItems
            .Select(item =>
            {
                var basePrice = item.Product?.Price ?? 0m;
                var discountPercentage = item.Product?.Discount?.Percentage ?? 0m;
                var finalPrice = basePrice * (1 - discountPercentage / 100);
                var lineTotal = finalPrice * item.Quantity;

                return new CartItemViewModel
                {
                    Id = item.Id,
                    ProductName = item.Product?.Name ?? string.Empty,
                    Quantity = item.Quantity,
                    UnitPrice = finalPrice,
                    LineTotal = lineTotal
                };
            })
            .ToList();

        return new CartViewModel
        {
            Items = items,
            TotalAmount = items.Sum(item => item.LineTotal)
        };
    }

    public async Task<bool> HasItemsAsync(string userId)
    {
        return await _context.CartItems.AnyAsync(ci => ci.Cart.UserId == userId);
    }

    public async Task ClearCartAsync(string userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || cart.CartItems.Count == 0)
        {
            return;
        }

        _context.CartItems.RemoveRange(cart.CartItems);
        await _context.SaveChangesAsync();
    }

    public async Task AddToCartAsync(string userId, int productId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += 1;
            _context.CartItems.Update(existingItem);
        }
        else
        {
            var cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = 1,
                CartId = cart.Id
            };
            _context.CartItems.Add(cartItem);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem == null)
        {
            return;
        }

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task IncreaseQuantityAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem == null)
        {
            return;
        }

        cartItem.Quantity += 1;
        _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task DecreaseQuantityAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem == null)
        {
            return;
        }

        cartItem.Quantity -= 1;
        if (cartItem.Quantity <= 0)
        {
            _context.CartItems.Remove(cartItem);
        }
        else
        {
            _context.CartItems.Update(cartItem);
        }

        await _context.SaveChangesAsync();
    }
}
