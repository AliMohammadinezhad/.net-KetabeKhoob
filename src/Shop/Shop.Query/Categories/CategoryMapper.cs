using Shop.Domain.CategoryAgg;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories;

internal static class CategoryMapper
{
    public static CategoryDto Map(this Category category)
    {
        return new CategoryDto(category.Id, category.CreationDate, category.Title,
            category.Slug, category.SeoData, category.ParentId, category.Childes.MapChildren());
    }

    public static List<CategoryDto> Map(this List<Category> categories)
    {
        var model = new List<CategoryDto>();
        categories.ForEach(category =>
        {
            model.Add(
                new CategoryDto(category.Id, category.CreationDate, category.Title,
                category.Slug, category.SeoData, category.ParentId, category.Childes.MapChildren())
            );
        });
        return model;
    }

    public static List<ChildCategoryDto> MapChildren(this List<Category> children)
    {
        var model = new List<ChildCategoryDto>();
        children.ForEach(x => model.Add(
            new ChildCategoryDto(x.Id, x.CreationDate, x.Title, x.Slug, x.SeoData,
                (long)x.ParentId!, x.Childes.MapSecondaryChildren())
        ));
        return model;
    }

    private static List<SecondaryChildCategoryDto> MapSecondaryChildren(this List<Category> children)
    {
        var model = new List<SecondaryChildCategoryDto>();
        children.ForEach(x => model.Add(
            new SecondaryChildCategoryDto(x.Id, x.CreationDate, x.Title, x.Slug, x.SeoData, (long)x.ParentId!)
            ));
        return model;
    }
}