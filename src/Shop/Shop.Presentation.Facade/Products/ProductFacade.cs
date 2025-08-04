using Common.Application;
using Common.CacheHelper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Products.Create;
using Shop.Application.Products.Delete;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Application.Products.RemoveImage.AddImage;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetForShop;

namespace Shop.Presentation.Facade.Products;

internal class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;
    private readonly ISellerInventoryFacade _inventoryFacade;

    public ProductFacade(IMediator mediator, IDistributedCache cache, ISellerInventoryFacade inventoryFacade)
    {
        _mediator = mediator;
        _cache = cache;
        _inventoryFacade = inventoryFacade;
    }

    public async Task<OperationResult> AddProductImage(AddProductImageCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (result.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId, cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Product(product.Slug), cancellationToken);
            await _cache.RemoveAsync(CacheKeys.ProductSingle(product.Slug), cancellationToken);

        }
        return result;
    }

    public async Task<OperationResult> CreateProduct(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);

    }

    public async Task<OperationResult> EditProduct(EditProductCommand command, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(CacheKeys.Product(command.Slug), cancellationToken);
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> DeleteProductById(long productId, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new DeleteProductCommand(productId), cancellationToken);
    }

    public async Task<OperationResult> RemoveProductImage(RemoveProductImageCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (result.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId, cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Product(product.Slug), cancellationToken);
            await _cache.RemoveAsync(CacheKeys.ProductSingle(product.Slug), cancellationToken);
        }
        return result;
    }

    public async Task<ProductDto?> GetProductById(long id, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
    }

    public async Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams), cancellationToken);

    }

    public async Task<ProductShopResult?> GetProductsForShop(ProductShopFilterParam filterParams, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetProductForShopQuery(filterParams), cancellationToken);
    }

    public async Task<ProductDto?> GetProductBySlug(string slug, CancellationToken cancellationToken = default)
    {
        return await _cache.GetOrSet(CacheKeys.Product(slug), () => 
            _mediator.Send(new GetProductBySlugQuery(slug), cancellationToken));
    }


    public async Task<SingleProductDto?> GetProductBySlugForSinglePage(string slug, CancellationToken cancellationToken = default)
    {
        return await _cache.GetOrSet(CacheKeys.Product(slug), async () =>
        {
            var product = await _mediator.Send(new GetProductBySlugQuery(slug), cancellationToken);
            if (product == null)
                return null;

            var inventories = await _inventoryFacade.GetSellerInventoryListByProductId(product.Id);
            var model = new SingleProductDto()
            {
                Inventories = inventories,
                Product = product
            };
            return model;
        });
    }
}