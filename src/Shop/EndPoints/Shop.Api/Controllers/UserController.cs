using AutoMapper;
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
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.ChangePassword;
using Shop.Domain.RoleAgg.Enums;

namespace Shop.Api.Controllers;

[Authorize]
public class UserController : ApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IMapper _mapper;

    public UserController(IUserFacade userFacade, IMapper mapper)
    {
        _userFacade = userFacade;
        _mapper = mapper;
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

    [HttpPut("ChangePassword")]
    public async Task<ApiResult> EditUser([FromBody] ChangePasswordViewModel  command)
    {
        var changePasswordModel = _mapper.Map<ChangeUserPasswordCommand>(command);
        var result = await _userFacade.ChangePassword(changePasswordModel);
        return CommandResult(result);
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
        command.UserId = User.GetUserId();
        var result = await _userFacade.EditUser(command);
        return CommandResult(result);
    }
}