using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Application.SiteEntities.Sliders.Edit;

public record EditSliderCommand(
    long Id,
    string Title,
    string Link,
    IFormFile? ImageFile,
    SliderPosition Position
    ) : IBaseCommand;