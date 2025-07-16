using Microsoft.EntityFrameworkCore;
using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Slider;

public class SliderRepository : BaseRepository<Domain.SiteEntities.Slider>, ISliderRepository
{
    private readonly ShopContext _context;
    public SliderRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public void DeleteSlider(Domain.SiteEntities.Slider slider)
    {
        _context.Sliders.Remove(slider);
    }
}