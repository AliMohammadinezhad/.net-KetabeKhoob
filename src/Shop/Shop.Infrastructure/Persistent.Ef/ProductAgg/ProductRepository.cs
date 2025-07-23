using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly ShopContext _context;
    public ProductRepository(ShopContext context) : base(context)
    {
        _context = context;
    }


    public void DeleteProduct(Product product)
    {
        _context.Remove(product);
    }
}