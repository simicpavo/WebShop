namespace WebShop.ViewModels;

public class CartItemDetailsViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int CartId { get; set; }
}
