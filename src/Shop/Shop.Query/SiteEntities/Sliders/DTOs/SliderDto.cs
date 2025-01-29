using Common.Query;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Query.SiteEntities.Sliders.DTOs;

public class SliderDto : BaseDto
{
    public string Title { get; set; }
    public string Link { get; set; }
    public string ImageName { get; set; }
    public SliderPositionDto Position { get; set; }
}