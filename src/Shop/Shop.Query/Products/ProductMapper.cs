using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products;

public static class ProductMapper
{
    public static ProductDto? Map(this Product? product)
    {
        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            Description = product.Description,
            ImageName = product.ImageName,
            Images = product.Images.Select(x => new ProductImageDto()
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                ImageName = x.ImageName,
                Order = x.Order,
                ProductId = x.ProductId
            }).ToList(),
            SeoData = product.SeoData,
            Slug = product.Slug,
            Title = product.Title,
            Specifications = product.Specifications.Select(x => new ProductSpecificationDto()
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                Key = x.Key,
                Value = x.Value
            }).ToList(),
            Category = new ProductCategoryDto()
            {
                Id = product.CategoryId
            },
            SubCategory = new ProductCategoryDto()
            {
                Id = product.SubCategoryId
            },
            SecondarySubCategory = product.SecondarySubCategoryId == null ? null : new ProductCategoryDto()
            {
                Id = (long)product.SecondarySubCategoryId
            },
        };
    }

    public static ProductFilterData? MapListData(this Product? product)
    {
        return new ProductFilterData()
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            Description = product.Description,
            ImageName = product.ImageName,
            SeoData = product.SeoData,
            Slug = product.Slug,
            Title = product.Title
        };
    }

    public static async Task SetCategories(this ProductDto product, ShopContext _context)
    {
        var categories = await _context.Categories
            .Where(x => x.Id == product.Category.Id || x.Id == product.SubCategory.Id)
            .Select(x => new ProductCategoryDto()
            {
                Id = x.Id,
                ParentId = x.ParentId,
                SeoData = x.SeoData,
                Title = x.Title,
                Slug = x.Slug,
            })
            .ToListAsync();


        if (product.SecondarySubCategory?.Id is not null)
        {
            var secondarySubCategory = await _context.Categories
                .Where(x => x.Id == product.SecondarySubCategory.Id)
                .Select(x => new ProductCategoryDto()
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    SeoData = x.SeoData,
                    Title = x.Title,
                    Slug = x.Slug,
                })
                .FirstOrDefaultAsync();
            if(secondarySubCategory is not null)
                product.SecondarySubCategory = secondarySubCategory;
        }

        product.Category = categories.FirstOrDefault(x => x.Id == product.Category.Id);
        product.SubCategory = categories.FirstOrDefault(x => x.Id == product.SubCategory.Id);

    }
}