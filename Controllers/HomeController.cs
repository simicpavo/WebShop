using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public HomeController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        var discountedProducts = await _productService.GetDiscountedProductsAsync();

        var model = new HomeView
        {
            Categories = categories.ToList(),
            DiscountedProducts = discountedProducts.ToList(),
        };

        return View(model);
    }
}
