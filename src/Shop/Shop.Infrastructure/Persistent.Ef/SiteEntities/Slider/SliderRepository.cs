using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Slider;

public class SliderRepository : BaseRepository<Domain.SiteEntities.Slider>, ISliderRepository
{
    public SliderRepository(ShopContext context) : base(context)
    {
    }
}