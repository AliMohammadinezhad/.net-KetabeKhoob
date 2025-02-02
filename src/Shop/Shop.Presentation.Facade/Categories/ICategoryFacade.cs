using Common.Application;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.Remove;
using Shop.Query.Categories.DTOs;

namespace Shop.Presentation.Facade.Categories;

public interface ICategoryFacade
{
    Task<OperationResult> AddChild(AddChildCategoryCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> Edit(EditCategoryCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> Remove(long categoryId, CancellationToken cancellationToken = default);


    Task<CategoryDto?> GetById(long id, CancellationToken cancellationToken = default);
    Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId, CancellationToken cancellationToken = default);
    Task<List<CategoryDto>?> GetCategories(CancellationToken cancellationToken = default);


}