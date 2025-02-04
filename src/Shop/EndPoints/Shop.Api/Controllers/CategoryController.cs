using System.Net;
using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.Remove;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories.DTOs;

namespace Shop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ApiController
{
    private readonly ICategoryFacade _categoryFacade;

    public CategoryController(ICategoryFacade categoryFacade)
    {
        _categoryFacade = categoryFacade;
    }

    [HttpGet]
    public async Task<ApiResult<List<CategoryDto>?>> GetCategories(CancellationToken cancellationToken)
    {
        var result = await _categoryFacade.GetCategories(cancellationToken);
        return QueryResult(result);
    }

    [HttpGet("{id:long}")]
    public async Task<ApiResult<CategoryDto?>> GetCategoryById([FromRoute]long id, CancellationToken cancellationToken)
    {
        var result = await _categoryFacade.GetById(id, cancellationToken);
        return QueryResult(result);
    }

    [HttpGet("getChild/{parentId:long}")]
    public async Task<ApiResult<List<ChildCategoryDto>?>> GetCategoriesByParentId([FromRoute] long parentId, CancellationToken cancellationToken)
    {
        var result = await _categoryFacade.GetCategoriesByParentId(parentId, cancellationToken);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult<long>> CreateCategory(CreateCategoryCommand command)
    {
        var result = await _categoryFacade.Create(command);
        var url = Url.Action("GetCategoryById", "Category", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPost("addChild")]
    public async Task<ApiResult<long>> AddChildCategory(AddChildCategoryCommand command)
    {
        var result = await _categoryFacade.AddChild(command);
        var url = Url.Action("GetCategoriesByParentId", "Category", new { parentId = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    public async Task<ApiResult> EditCategory(EditCategoryCommand command)
    {
        var result = await _categoryFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete("{categoryId:long}")]
    public async Task<ApiResult> RemoveCategory(long categoryId)
    {
        var result = await _categoryFacade.Remove(categoryId);
        return CommandResult(result);
    }
}