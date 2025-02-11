using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Application.Products.RemoveImage.AddImage;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.CrudProduct)]
public class ProductController : ApiController
{
    private readonly IProductFacade _productFacade;

    public ProductController(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<ProductFilterResult?>> GetProductByFilter([FromQuery] ProductFilterParams filterParams)
    {
        var product = await _productFacade.GetProductByFilter(filterParams);
        return QueryResult(product);
    }

    [AllowAnonymous]
    [HttpGet("Shop")]
    public async Task<ApiResult<ProductShopResult?>> GetProductForShopFilter([FromQuery] ProductShopFilterParam filterParams)
    {
        var product = await _productFacade.GetProductsForShop(filterParams);
        return QueryResult(product);
    }

    [HttpGet("{productId:long}")]
    public async Task<ApiResult<ProductDto?>> GetProductById([FromRoute] long productId)
    {
        var product = await _productFacade.GetProductById(productId);
        return QueryResult(product);
    }

    [AllowAnonymous]
    [HttpGet("{productSlug}")]
    public async Task<ApiResult<ProductDto?>> GetProductBySlug([FromRoute] string productSlug)
    {
        var product = await _productFacade.GetProductBySlug(productSlug);
        return QueryResult(product);
    }

    [HttpPost]
    public async Task<ApiResult> CreateProduct([FromForm] CreateProductCommand command)
    {
        var result = await _productFacade.CreateProduct(command);
        return CommandResult(result);
    }

    [HttpPost("images")]
    public async Task<ApiResult> AddProductImage([FromForm] AddProductImageCommand command)
    {
        var result = await _productFacade.AddProductImage(command);
        return CommandResult(result);
    }

    [HttpDelete("images")]
    public async Task<ApiResult> RemoveProductImage(RemoveProductImageCommand command)
    {
        var result = await _productFacade.RemoveProductImage(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditProduct([FromForm] EditProductCommand command)
    {
        var result = await _productFacade.EditProduct(command);
        return CommandResult(result);
    }
}