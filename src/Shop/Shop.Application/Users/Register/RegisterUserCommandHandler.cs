using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Register;

public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUserDomainService userDomainService
    )
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = Sha256Hasher.Hash(request.Password);
        var user = User.RegisterUser(request.PhoneNumber.Value, hashedPassword, _userDomainService);

        await _userRepository.AddAsync(user);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}