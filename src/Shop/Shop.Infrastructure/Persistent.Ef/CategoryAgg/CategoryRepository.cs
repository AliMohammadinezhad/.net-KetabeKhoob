using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.CategoryAgg;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly ShopContext _context;
    public CategoryRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> DeleteCategory(long categoryId)
    {
        var category = await _context.Categories
            .Include(x => x.Childes)
            .ThenInclude(x => x.Childes)
            .FirstOrDefaultAsync(x => x.Id == categoryId);
        if (category is null)
            return false;

        var productIsExist = await _context.Products
            .AnyAsync(x => x.CategoryId == category.Id ||
                           x.SubCategoryId == category.Id || 
                           x.SecondarySubCategoryId == category.Id);

        if (productIsExist)
            return false;

        if (category.Childes.Any(x => x.Childes.Any()))
            _context.RemoveRange(category.Childes.SelectMany(x => x.Childes));

        if (category.Childes.Any())
            _context.RemoveRange(category.Childes);
        
        _context.RemoveRange(category);
        return true;
    }
}