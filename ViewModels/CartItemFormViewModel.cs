using System.ComponentModel.DataAnnotations;

namespace WebShop.ViewModels;

public class CartItemFormViewModel
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public int CartId { get; set; }
}
