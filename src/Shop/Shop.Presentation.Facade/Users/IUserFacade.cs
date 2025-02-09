using Common.Application;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.ChargeWallet;
using Shop.Application.Users.Create;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.Edit;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.Register;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users;

public interface IUserFacade
{
    Task<OperationResult> ChargeUserWallet(ChargeUserWalletCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> CreateUser(CreateUserCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> EditUser(EditUserCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> RegisterUser(RegisterUserCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> AddUserToken(AddUserTokenCommand command, CancellationToken cancellationToken = default);


    Task<UserFilterResult> GetUserByFilter(UserFilterParams  filterParams, CancellationToken cancellationToken = default);
    Task<UserDto?> GetUserById(long id, CancellationToken cancellationToken = default);
    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);

}