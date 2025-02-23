using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Services;

public interface IDiscountService
{
    Task<IEnumerable<Discount>> GetAllDiscountsAsync();
    Task<Discount> GetDiscountByIdAsync(int id);
    Task CreateDiscountAsync(Discount discount);
    Task UpdateDiscountAsync(Discount discount);
    Task DeleteDiscountAsync(int id);
}
