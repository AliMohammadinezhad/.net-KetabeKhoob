using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Enums;
using Shop.Query.SiteEntities.Banners.DTOs;

namespace Shop.Query.SiteEntities.Banners;

public static class BannerMapper
{
    public static BannerDto? Map(this Banner? banner)
    {
        if (banner == null) return null;
        return new BannerDto
        {
            ImageName = banner.ImageName,
            Link = banner.Link,
            Position = MapBannerPosition(banner.Position),
            Id = banner.Id,
            CreationDate = banner.CreationDate
        };
    }

    private static BannerPositionDto MapBannerPosition(BannerPosition position)
    {
        return position switch
        {
            BannerPosition.Top => BannerPositionDto.Top,
            BannerPosition.Right => BannerPositionDto.Right,
            BannerPosition.Left => BannerPositionDto.Left,
            BannerPosition.Bottom => BannerPositionDto.Bottom,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }
}