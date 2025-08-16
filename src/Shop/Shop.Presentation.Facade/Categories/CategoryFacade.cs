using Common.Application;
using Common.CacheHelper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.Remove;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.GetById;
using Shop.Query.Categories.GetByParentId;
using Shop.Query.Categories.GetList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Categories;

internal class CategoryFacade : ICategoryFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _distributedCache;
    public CategoryFacade(IMediator mediator, IDistributedCache distributedCache)
    {
        _mediator = mediator;
        _distributedCache = distributedCache;
    }

    public async Task<OperationResult<long>> AddChild(AddChildCategoryCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> Edit(EditCategoryCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult<long>> Create(CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> Remove(long categoryId, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new RemoveCategoryCommand(categoryId), cancellationToken);
    }

    public async Task<CategoryDto?> GetById(long id, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
    }

    public async Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetCategoryByParentIdQuery(parentId), cancellationToken);
    }

    public async Task<List<CategoryDto>?> GetCategories(CancellationToken cancellationToken = default)
    {
        return await _distributedCache.GetOrSet(CacheKeys.Categories, async () => await _mediator.Send(new GetCategoryListQuery(), cancellationToken));
    }
}