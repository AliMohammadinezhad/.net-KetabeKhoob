using Common.Application;
using Common.Application.SecurityUtil;
using MediatR;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.ChargeWallet;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.GetByPhoneNumber;
using Shop.Query.Users.UserTokens;

namespace Shop.Presentation.Facade.Users;

internal class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;

    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> ChargeUserWallet(ChargeUserWalletCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> CreateUser(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

   public async Task<OperationResult> EditUser(EditUserCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

   
    public async Task<OperationResult> RegisterUser(RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> AddUserToken(AddUserTokenCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> RemoveUserToken(RemoveUserTokenCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams), cancellationToken);
    }

    public async Task<UserDto?> GetUserById(long id, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
    }

    public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken, CancellationToken cancellationToken = default)
    {
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
        return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken), cancellationToken);
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber), cancellationToken);
    }
}