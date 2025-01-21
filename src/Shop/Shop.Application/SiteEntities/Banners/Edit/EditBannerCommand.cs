using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Application.SiteEntities.Banners.Edit;

public record EditBannerCommand(
    long Id,
    string Link,
    IFormFile? ImageFile,
    BannerPosition Position) : IBaseCommand;