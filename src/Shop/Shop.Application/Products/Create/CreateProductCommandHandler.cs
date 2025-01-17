using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Create;

public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
{
    private readonly IProductDomainService _productDomainService;
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public CreateProductCommandHandler(
        IProductDomainService productDomainService,
        IProductRepository productRepository,
        IFileService fileService)
    {
        _productDomainService = productDomainService;
        _productRepository = productRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
        var product = new Product(request.Title, imageName, request.Description, request.CategoryId,
            request.SecondarySubCategoryId, request.SecondarySubCategoryId,request.Slug, request.SeoData, _productDomainService);

        await _productRepository.AddAsync(product);

        var specifications = new List<ProductSpecification>();
        request.Specifications.ToList().ForEach(specification =>
        {
            specifications.Add(new ProductSpecification(specification.Key, specification.Value));
        });
        product.SetSpecification(specifications);
        await _productRepository.Save();
        return OperationResult.Success();
    }
}