using Common.Domain.ValueObjects;
using Common.Query;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Products.DTOs;

public class ProductFilterData : BaseDto
{
    public string Title { get;  set; }
    public string ImageName { get;  set; }
    public string Description { get;  set; }
    public string Slug { get;  set; }
    public SeoData SeoData { get;  set; }
}