using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Domain.SiteEntities;

public class Slider : BaseEntity
{
    public string Title { get; private set; }
    public string Link { get; private set; }
    public string ImageName { get; private set; }
    public SliderPosition Position { get; private set; }

    private Slider()
    {
    }

    public Slider(string title, string link, string imageName, SliderPosition position)
    {
        Guard(title, link, imageName);
        Title = title;
        Link = link;
        ImageName = imageName;
        Position = position;
    }

    public void Edit(string title, string link, string imageName, SliderPosition position)
    {
        Guard(title, link, imageName);
        Title = title;
        Link = link;
        ImageName = imageName;
        Position = position;
    }

    private void Guard(string title, string link, string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(link, nameof(link));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
    }
}