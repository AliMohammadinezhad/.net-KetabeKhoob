using Common.Query;
using Shop.Query.SiteEntities.Banners.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetById;

public record GetBannerByIdQuery(long BannerId) : IQuery<BannerDto?>;