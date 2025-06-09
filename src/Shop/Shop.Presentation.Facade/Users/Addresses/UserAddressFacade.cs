using Common.Application;
using MediatR;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.SetActiveAddress;
using Shop.Query.Users.Addresses.GetById;
using Shop.Query.Users.Addresses.GetList;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users.Addresses;

internal class UserAddressFacade : IUserAddressFacade
{
    private readonly IMediator _mediator;

    public UserAddressFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddUserAddress(AddUserAddressCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> DeleteUserAddress(DeleteUserAddressCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> EditUserAddress(EditUserAddressCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> SetActiveUserAddress(SetActiveUserAddressCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<AddressDto?> GeyById(long userAddressId)
    {
        return await _mediator.Send(new GetUserAddressByIdQuery(userAddressId));
    }

    public async Task<List<AddressDto?>> GeyList(long userId)
    {
        return await _mediator.Send(new GetUserAddressListQuery(userId));
    }
}