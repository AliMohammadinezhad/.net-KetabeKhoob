using Common.Application;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.SetActiveAddress;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users.Addresses;

public interface IUserAddressFacade
{
    Task<OperationResult> AddUserAddress(AddUserAddressCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> DeleteUserAddress(DeleteUserAddressCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> EditUserAddress(EditUserAddressCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> SetActiveUserAddress(SetActiveUserAddressCommand command, CancellationToken cancellationToken = default);

    Task<AddressDto?> GeyById(long userAddressId);
    Task<List<AddressDto?>> GeyList(long userId);
}