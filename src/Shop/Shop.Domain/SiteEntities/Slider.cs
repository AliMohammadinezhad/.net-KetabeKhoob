using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Domain.SiteEntities;

public class Slider : BaseEntity
{
    public string Link { get; private set; }
    public string ImageName { get; private set; }
    public SliderPosition Position { get; private set; }

    private Slider()
    {
    }

    public Slider(string link, string imageName, SliderPosition position)
    {
        Guard(link, imageName);
        Link = link;
        ImageName = imageName;
        Position = position;
    }

    public void Edit(string link, string imageName, SliderPosition position)
    {
        Guard(link, imageName);
        Link = link;
        ImageName = imageName;
        Position = position;
    }

    private void Guard(string link, string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(link, nameof(link));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
    }
}