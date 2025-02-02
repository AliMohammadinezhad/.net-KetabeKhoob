using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products;

public class ProductDomainService : IProductDomainService
{
    private readonly IProductRepository _productRepository;

    public ProductDomainService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool SlugIsExist(string slug)
    {
        return _productRepository.Exists(x => x.Slug == slug);
    }
}