using System.Threading.Tasks;
using WebShop.ViewModels;

namespace WebShop.Services;

public interface ICartService
{
    Task<CartViewModel> GetCartViewModelAsync(string userId);
    Task<bool> HasItemsAsync(string userId);
    Task ClearCartAsync(string userId);
    Task AddToCartAsync(string userId, int productId);
    Task RemoveItemAsync(int cartItemId);
    Task IncreaseQuantityAsync(int cartItemId);
    Task DecreaseQuantityAsync(int cartItemId);
}
