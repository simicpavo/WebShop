using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Services;

public class DiscountService : IDiscountService
{
    private readonly ApplicationDbContext _context;

    public DiscountService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Discount>> GetAllDiscountsAsync()
    {
        return await _context.Discounts.ToListAsync();
    }

    public async Task<Discount> GetDiscountByIdAsync(int id)
    {
        return await _context.Discounts.FindAsync(id);
    }

    public async Task CreateDiscountAsync(Discount discount)
    {
        _context.Discounts.Add(discount);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDiscountAsync(Discount discount)
    {
        _context.Discounts.Update(discount);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDiscountAsync(int id)
    {
        var discount = await _context.Discounts.FindAsync(id);
        if (discount != null)
        {
            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
        }
    }
}
