namespace WebShop.Models
{
    public class HomeView
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Product> DiscountedProducts { get; set; } = new List<Product>();
    }
}
