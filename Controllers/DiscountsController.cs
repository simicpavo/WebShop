using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;

[Authorize(Roles = "Admin")]
public class DiscountsController : Controller
{
    private readonly IDiscountService _discountService;

    public DiscountsController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    // GET: Discount
    public async Task<IActionResult> Index()
    {
        var discounts = await _discountService.GetAllDiscountsAsync();
        return View(discounts);
    }

    // GET: Discount/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Discount/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Discount discount)
    {
        if (ModelState.IsValid)
        {
            await _discountService.CreateDiscountAsync(discount);
            return RedirectToAction(nameof(Index));
        }
        return View(discount);
    }

    // GET: Discount/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var discount = await _discountService.GetDiscountByIdAsync(id);
        if (discount == null)
        {
            return NotFound();
        }
        return View(discount);
    }

    // POST: Discount/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Discount discount)
    {
        if (id != discount.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _discountService.UpdateDiscountAsync(discount);
            return RedirectToAction(nameof(Index));
        }
        return View(discount);
    }

    // GET: Discount/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var discount = await _discountService.GetDiscountByIdAsync(id);
        if (discount == null)
        {
            return NotFound();
        }
        return View(discount);
    }

    // POST: Discount/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _discountService.DeleteDiscountAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
