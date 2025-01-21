using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Application.SiteEntities.Sliders.Create;

public record CreateSliderCommand(
    string Title,
    string Link,
    IFormFile ImageFile,
    SliderPosition Position
    ) : IBaseCommand;