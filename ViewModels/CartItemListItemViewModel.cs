namespace WebShop.ViewModels;

public class CartItemListItemViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int CartId { get; set; }
}
