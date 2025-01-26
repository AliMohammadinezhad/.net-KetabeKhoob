using Common.Domain.ValueObjects;
using Common.Query;

namespace Shop.Query.Categories.DTOs;

public class CategoryDto : BaseDto
{
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public long? ParentId { get; private set; }
    public List<ChildCategoryDto> Children { get; private set; }

    public CategoryDto(
        long id,
        DateTime creationDate,
        string title,
        string slug,
        SeoData seoData,
        long? parentId,
        List<ChildCategoryDto> children
        ) : base(id, creationDate)
    {
        Title = title;
        Slug = slug;
        SeoData = seoData;
        ParentId = parentId;
        Children = children;
    }
}