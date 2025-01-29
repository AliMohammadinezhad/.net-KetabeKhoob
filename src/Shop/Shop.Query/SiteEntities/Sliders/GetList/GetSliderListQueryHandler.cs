using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.Sliders.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetList;

public class GetSliderListQueryHandler : IQueryHandler<GetSliderListQuery, List<SliderDto?>>
{
    private readonly ShopContext _context;

    public GetSliderListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<SliderDto?>> Handle(GetSliderListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sliders
            .Select(x => x.Map())
            .ToListAsync(cancellationToken);
    }
}