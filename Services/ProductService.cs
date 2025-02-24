using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context
            .Products.Include(p => p.Category)
            .Include(p => p.Discount)
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context
            .Products.Include(p => p.Category)
            .Include(p => p.Discount)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Product>> GetDiscountedProductsAsync()
    {
        return await _context
            .Products.Include(p => p.Category)
            .Include(p => p.Discount)
            .Where(p => p.Discount != null && p.Discount.Percentage > 0) 
            .OrderByDescending(p => p.Discount.Percentage) 
            .ToListAsync();
    }
}
