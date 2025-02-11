using Common.Application;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Application.Products.RemoveImage.AddImage;
using Shop.Query.Products.DTOs;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{
    Task<OperationResult> AddProductImage(AddProductImageCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> CreateProduct(CreateProductCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> EditProduct(EditProductCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> RemoveProductImage(RemoveProductImageCommand command, CancellationToken cancellationToken = default);
    
    
    Task<ProductDto?> GetProductById(long id, CancellationToken cancellationToken = default);
    Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams, CancellationToken cancellationToken = default);
    Task<ProductShopResult?> GetProductsForShop(ProductShopFilterParam filterParams,
        CancellationToken cancellationToken = default);
    Task<ProductDto?> GetProductBySlug(string slug, CancellationToken cancellationToken = default);
}