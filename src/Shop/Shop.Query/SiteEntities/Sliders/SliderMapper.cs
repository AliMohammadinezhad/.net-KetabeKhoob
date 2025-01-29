using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Enums;
using Shop.Query.SiteEntities.Sliders.DTOs;

namespace Shop.Query.SiteEntities.Sliders;

public static class SliderMapper
{
    public static SliderDto? Map(this Slider? slider)
    {
        if (slider == null) return null;
        return new SliderDto()
        {
            ImageName = slider.ImageName,
            Link = slider.Link,
            Title = slider.Title,
            Position = SliderPositionMapper(slider.Position),
            Id = slider.Id,
            CreationDate = slider.CreationDate,
        };
    }

    private static SliderPositionDto SliderPositionMapper(SliderPosition position)
    {
        return position switch
        {
            SliderPosition.Left => SliderPositionDto.Left,
            SliderPosition.Top => SliderPositionDto.Top,
            SliderPosition.Right => SliderPositionDto.Right,
            SliderPosition.Bottom => SliderPositionDto.Bottom,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }
}