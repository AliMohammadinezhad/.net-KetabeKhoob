using Shop.Domain.CategoryAgg;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.CategoryAgg;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ShopContext context) : base(context)
    {
    }
}