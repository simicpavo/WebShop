using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IDiscountService _discountService;

    public ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        IDiscountService discountService
    )
    {
        _productService = productService;
        _categoryService = categoryService;
        _discountService = discountService;
    }

    [Authorize(Roles = "Admin")]
    // GET: Products
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }

    public async Task<IActionResult> AllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        // Dohvati kategorije i popuste
        var categories = await _categoryService.GetAllCategoriesAsync();
        var discounts = await _discountService.GetAllDiscountsAsync();

        // Dodaj kategorije i popuste u ViewBag
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        ViewBag.Discounts = new SelectList(discounts, "Id", "Name");

        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            
            // Ponovno popuni ViewBag
            var categories = await _categoryService.GetAllCategoriesAsync();
            var discounts = await _discountService.GetAllDiscountsAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Discounts = new SelectList(discounts, "Id", "Name");

            return View(product);
        }

        await _productService.CreateProductAsync(product);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories = (await _categoryService.GetAllCategoriesAsync())
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToList();

        ViewBag.Discounts = (await _discountService.GetAllDiscountsAsync())
            .Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = $"{d.Name} ({d.Percentage}%)",
            })
            .ToList();

        return View(product);
    }

    [Authorize(Roles = "Admin")]
    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _productService.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [Authorize(Roles = "Admin")]
    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [Authorize(Roles = "Admin")]
    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteProductAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
