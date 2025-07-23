using Common.Application;
using MediatR;
using Shop.Application.Products.Create;
using Shop.Application.Products.Delete;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Application.Products.RemoveImage.AddImage;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetForShop;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Products;

internal class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;

    public ProductFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddProductImage(AddProductImageCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> CreateProduct(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);

    }

    public async Task<OperationResult> EditProduct(EditProductCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> DeleteProductById(long productId, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new DeleteProductCommand(productId), cancellationToken);
    }

    public async Task<OperationResult> RemoveProductImage(RemoveProductImageCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
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
        return await _mediator.Send(new GetProductBySlugQuery(slug), cancellationToken);
    }
}