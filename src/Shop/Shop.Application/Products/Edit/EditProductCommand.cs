using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Products.Edit;


public record EditProductCommand(
    long ProductId,
    string Title,
    IFormFile? ImageFile,
    string Description,
    long CategoryId,
    long SubCategoryId,
    long SecondarySubCategoryId,
    string Slug,
    SeoData SeoData,
    Dictionary<string, string> Specifications
    ) : IBaseCommand;