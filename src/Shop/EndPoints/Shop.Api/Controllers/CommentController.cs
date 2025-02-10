using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments.DTOs;

namespace Shop.Api.Controllers;

public class CommentController : ApiController
{
    private readonly ICommentFacade _commentFacade;

    public CommentController(ICommentFacade commentFacade)
    {
        _commentFacade = commentFacade;
    }

    [PermissionChecker(Permission.CommentManagement)]
    [HttpGet]
    public async Task<ApiResult<CommentFilterResult?>> GetCommentsByFilter([FromQuery] CommentFilterParams filterParams)
    {
        var result = await _commentFacade.GetCommentByFilter(filterParams);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.CommentManagement)]
    [HttpGet("{commentId:long}")]
    public async Task<ApiResult<CommentDto?>> GetCommentsById(long commentId)
    {
        var result = await _commentFacade.GetCommentById(commentId);
        return QueryResult(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<ApiResult> CreateComment([FromBody] CreateCommentCommand command)
    {
        var result = await _commentFacade.CreateComment(command);
        return CommandResult(result);
    }

    [Authorize]
    [HttpPut]
    public async Task<ApiResult> EditComment([FromBody] EditCommentCommand command)
    {
        var result = await _commentFacade.EditComment(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.CommentManagement)]
    [HttpPut("changeStatus")]
    public async Task<ApiResult> ChangeCommentStatus([FromBody] ChangeCommentStatusCommand command)
    {
        var result = await _commentFacade.ChangeStatus(command);
        return CommandResult(result);
    }
}