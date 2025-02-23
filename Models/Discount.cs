namespace WebShop.Models;

public class Discount
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required decimal Percentage { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
