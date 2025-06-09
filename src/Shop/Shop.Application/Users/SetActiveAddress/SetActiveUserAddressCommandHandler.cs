using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.SetActiveAddress;

public class SetActiveUserAddressCommandHandler : IBaseCommandHandler<SetActiveUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public SetActiveUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(SetActiveUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user is null)
            return OperationResult.NotFound();

        user.SetActiveAddress(request.AddressId);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}