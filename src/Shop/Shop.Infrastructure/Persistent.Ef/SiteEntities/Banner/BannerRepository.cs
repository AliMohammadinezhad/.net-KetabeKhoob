using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Banner;

public class BannerRepository : BaseRepository<Domain.SiteEntities.Banner>, IBannerRepository
{
    private readonly ShopContext _context;
    public BannerRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public void DeleteBanner(Domain.SiteEntities.Banner slider)
    {
        _context.Banners.Remove(slider);
    }
}