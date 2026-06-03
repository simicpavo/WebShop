using Microsoft.AspNetCore.Identity;

namespace WebShop.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
