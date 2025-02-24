using System.ComponentModel.DataAnnotations;

namespace WebShop.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string? ImagePath { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public int? DiscountId { get; set; }
    public Discount? Discount { get; set; }
}
