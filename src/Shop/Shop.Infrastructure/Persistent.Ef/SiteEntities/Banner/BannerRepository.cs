using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Banner;

public class BannerRepository : BaseRepository<Domain.SiteEntities.Banner>, IBannerRepository
{
    public BannerRepository(ShopContext context) : base(context)
    {
    }
}