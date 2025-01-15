using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.AddChild;

public class AddChildCategoryCommandHandler : IBaseCommandHandler<AddChildCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public AddChildCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult> Handle(AddChildCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetTracking(request.ParentId);
        if (category is null)
            return OperationResult.NotFound();

        category.AddChild(request.Title, request.Slug, request.SeoData, _categoryDomainService);
        await _categoryRepository.Save();

        return OperationResult.Success();
    }
}