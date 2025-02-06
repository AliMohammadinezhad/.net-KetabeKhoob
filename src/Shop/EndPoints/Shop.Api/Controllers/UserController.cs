using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.ChargeWallet;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers;

public class UserController : ApiController
{
    private readonly IUserFacade _userFacade;

    public UserController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [HttpGet]
    public async Task<ApiResult<UserFilterResult?>> GetUserByFilter([FromQuery] UserFilterParams filterParams)
    {
        var result = await _userFacade.GetUserByFilter(filterParams);
        return QueryResult(result);
    }


    [HttpGet("{userId}")]
    public async Task<ApiResult<UserDto?>> GetUserById(long userId)
    {
        var result = await _userFacade.GetUserById(userId);
        return QueryResult(result);
    }

    [HttpGet("{phoneNumber}")]
    public async Task<ApiResult<UserDto?>> GetUserByPhoneNumber(string phoneNumber)
    {
        var result = await _userFacade.GetUserByPhoneNumber(phoneNumber);
        return QueryResult(result);
    }


    [HttpPost("register")]
    public async Task<ApiResult> RegisterUser(RegisterUserCommand command)
    {
        var result = await _userFacade.RegisterUser(command);
        return CommandResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _userFacade.CreateUser(command);
        return CommandResult(result);
    }

    [HttpPut("ChargeWallet")]
    public async Task<ApiResult> ChargeUserWallet([FromBody] ChargeUserWalletCommand command)
    {
        var result = await _userFacade.ChargeUserWallet(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditUser([FromBody] EditUserCommand command)
    {
        var result = await _userFacade.EditUser(command);
        return CommandResult(result);
    }
}