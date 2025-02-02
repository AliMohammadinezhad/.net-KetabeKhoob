using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories;

public class CategoryDomainService : ICategoryDomainService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryDomainService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public bool IsSlugExist(string slug)
    {
        return _categoryRepository.Exists(x => x.Slug == slug);
    }
}