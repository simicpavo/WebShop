using System.ComponentModel.DataAnnotations;

namespace WebShop.ViewModels;

public class CheckoutViewModel
{
    [Required]
    [Display(Name = "Ime")]
    [RegularExpression(@"^[a-zA-ZčćžšđČĆŽŠĐ\s-]+$",
    ErrorMessage = "Dozvoljena su samo slova, razmak i crtica.")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Prezime")]
    [RegularExpression(@"^[a-zA-ZčćžšđČĆŽŠĐ\s-]+$",
    ErrorMessage = "Dozvoljena su samo slova, razmak i crtica.")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "Broj kartice")]
    [RegularExpression("^\\d{16}$", ErrorMessage = "Broj kartice mora imati točno 16 znamenki.")]
    public string CardNumber { get; set; }

    [Required]
    [Display(Name = "Istek kartice (MM/GG)")]
    [RegularExpression("^(0[1-9]|1[0-2])/\\d{2}$", ErrorMessage = "Istek mora biti u formatu MM/GG.")]
    public string Expiry { get; set; }

    [Required]
    [Display(Name = "CVC")]
    [RegularExpression("^\\d{3,4}$", ErrorMessage = "CVC mora imati 3 ili 4 znamenke.")]
    public string Cvc { get; set; }
}
