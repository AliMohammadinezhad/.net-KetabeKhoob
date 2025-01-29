using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.Banners.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetList;

public class GetBannerListQueryHandler : IQueryHandler<GetBannerListQuery, List<BannerDto?>>
{
    private readonly ShopContext _context;

    public GetBannerListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<BannerDto?>> Handle(GetBannerListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Banners
            .OrderByDescending(x => x.Id)
            .Select(x => x.Map())
            .ToListAsync(cancellationToken);
    }
}