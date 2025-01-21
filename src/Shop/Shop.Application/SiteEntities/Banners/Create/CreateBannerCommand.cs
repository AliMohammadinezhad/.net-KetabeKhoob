using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Application.SiteEntities.Banners.Create;

public record CreateBannerCommand(
    string Link,
    IFormFile ImageFile,
    BannerPosition Position
    ) : IBaseCommand;