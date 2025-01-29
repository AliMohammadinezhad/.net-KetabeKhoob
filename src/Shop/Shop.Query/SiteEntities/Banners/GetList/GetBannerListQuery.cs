using Common.Query;
using Shop.Query.SiteEntities.Banners.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetList;

public record GetBannerListQuery : IQuery<List<BannerDto?>>;