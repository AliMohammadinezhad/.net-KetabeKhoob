using Common.Domain.ValueObjects;
using Common.Query;

namespace Shop.Query.Categories.DTOs;

public class SecondaryChildCategoryDto(long Id, DateTime CreationDate, string Title, string Slug, SeoData SeoData, long ParentId) : BaseDto;