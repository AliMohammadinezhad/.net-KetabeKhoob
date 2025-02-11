using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.ChargeWallet;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using Shop.Api.Infrastructure.Security;
using Shop.Domain.RoleAgg.Enums;

namespace Shop.Api.Controllers;

[Authorize]
public class UserController : ApiController
{
    private readonly IUserFacade _userFacade;

    public UserController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [HttpGet]
    [PermissionChecker(Permission.UserManagement)]
    public async Task<ApiResult<UserFilterResult?>> GetUserByFilter([FromQuery] UserFilterParams filterParams)
    {
        var result = await _userFacade.GetUserByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("Current")]
    public async Task<ApiResult<UserDto?>> GetCurrentUser()
    {
        var result = await _userFacade.GetUserById(User.GetUserId());
        return QueryResult(result);
    }


    [HttpGet("{userId}")]
    [PermissionChecker(Permission.UserManagement)]
    public async Task<ApiResult<UserDto?>> GetUserById(long userId)
    {
        var result = await _userFacade.GetUserById(userId);
        return QueryResult(result);
    }

    [HttpGet("{phoneNumber}")]
    [PermissionChecker(Permission.UserManagement)]
    public async Task<ApiResult<UserDto?>> GetUserByPhoneNumber(string phoneNumber)
    {
        var result = await _userFacade.GetUserByPhoneNumber(phoneNumber);
        return QueryResult(result);
    }


    [HttpPost("Register")]
    [PermissionChecker(Permission.UserManagement)]
    public async Task<ApiResult> RegisterUser(RegisterUserCommand command)
    {
        var result = await _userFacade.RegisterUser(command);
        return CommandResult(result);
    }

    [HttpPost]
    [PermissionChecker(Permission.UserManagement)]
    public async Task<ApiResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _userFacade.CreateUser(command);
        return CommandResult(result);
    }

    [HttpPut("ChargeWallet")]
    [PermissionChecker(Permission.UserManagement)]
    public async Task<ApiResult> ChargeUserWallet([FromBody] ChargeUserWalletCommand command)
    {
        var result = await _userFacade.ChargeUserWallet(command);
        return CommandResult(result);
    }

    [HttpPut]
    [PermissionChecker(Permission.UserManagement)]
    public async Task<ApiResult> EditUser([FromBody] EditUserCommand command)
    {
        var result = await _userFacade.EditUser(command);
        return CommandResult(result);
    }
}