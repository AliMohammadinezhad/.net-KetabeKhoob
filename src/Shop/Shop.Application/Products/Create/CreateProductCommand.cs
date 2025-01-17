using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Products.Create;

public record CreateProductCommand(
    string Title,
    IFormFile ImageFile,
    string Description,
    long CategoryId,
    long SubCategoryId,
    long SecondarySubCategoryId,
    string Slug,
    SeoData SeoData,
    Dictionary<string, string> Specifications
    ) : IBaseCommand;