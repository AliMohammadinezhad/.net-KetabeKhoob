using AutoMapper;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers;

[Authorize]
public class UserAddressController : ApiController
{
    private readonly IUserAddressFacade _userAddressFacade;
    private readonly IMapper _mapper;

    public UserAddressController(IUserAddressFacade userAddressFacade, IMapper mapper)
    {
        _userAddressFacade = userAddressFacade;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ApiResult<List<AddressDto?>?>> GetAddressList()
    {
        var query = await _userAddressFacade.GeyList(User.GetUserId());
        return QueryResult(query);
    }

    [HttpGet("{id:long}")]
    public async Task<ApiResult<AddressDto?>> GetAddressById(long id)
    {
        var query = await _userAddressFacade.GeyById(id);
        return QueryResult(query);
    }

    [HttpPost]
    public async Task<ApiResult> AddAddress(AddUserAddressViewModel viewModel)
    {
        var command = _mapper.Map<AddUserAddressCommand>(viewModel);
        command = command with { UserId = User.GetUserId() }; 
        var result = await _userAddressFacade.AddUserAddress(command);
        return CommandResult(result);
    }

    [HttpDelete("{addressId}")]
    public async Task<ApiResult> DeleteAddress(long addressId)
    {
        const int userId = 1;
        var result = await _userAddressFacade.DeleteUserAddress(new DeleteUserAddressCommand(userId, addressId));
        return CommandResult(result);
    }


    [HttpPut]
    public async Task<ApiResult> EditAddress(EditUserAddressViewModel viewModel)
    {
        var command = _mapper.Map<EditUserAddressCommand>(viewModel);
        command = command with { UserId = User.GetUserId() };
        var result = await _userAddressFacade.EditUserAddress(command);
        return CommandResult(result);
    }
}