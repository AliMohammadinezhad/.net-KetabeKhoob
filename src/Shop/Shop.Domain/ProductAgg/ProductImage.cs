using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.ProductAgg;

public class ProductImage : BaseEntity
{
    public long ProductId { get; internal set; }
    public string ImageName { get; private set; }
    public int Order { get; private set; }

    private ProductImage()
    {
    }
    public ProductImage(string imageName, int order)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        ImageName = imageName;
        Order = order;
    }
}