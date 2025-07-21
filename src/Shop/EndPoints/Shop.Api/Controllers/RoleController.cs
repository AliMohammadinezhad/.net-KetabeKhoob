using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Roles;
using Shop.Query.Roles.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.RoleManagement)]
public class RoleController : ApiController
{
    private readonly IRoleFacade _roleFacade;

    public RoleController(IRoleFacade roleFacade)
    {
        _roleFacade = roleFacade;
    }

    [HttpGet]
    public async Task<ApiResult<List<RoleDto>?>> GetRoleList()
    {
        var result = await _roleFacade.GetRoleList();
        return QueryResult(result);
    }

    [HttpGet("{roleId:long}")]
    public async Task<ApiResult<RoleDto?>> GetRoleById(long roleId)
    {
        var result = await _roleFacade.GetRoleById(roleId);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> CreateRole([FromBody] CreateRoleCommand command)
    {
        var result = await _roleFacade.CreateRole(command);
        return CommandResult(result);
    }


    [HttpPut]
    public async Task<ApiResult> EditRole([FromBody] EditRoleCommand command)
    {
        var result = await _roleFacade.EditRole(command);
        return CommandResult(result);
    }

    [HttpDelete("{roleId:long}")]
    public async Task<ApiResult> DeleteRole(long roleId)
    {
        var result = await _roleFacade.DeleteRole(roleId);
        return CommandResult(result);
    }
}