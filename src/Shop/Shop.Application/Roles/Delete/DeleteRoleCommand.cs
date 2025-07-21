using Common.Application;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Delete;

public record DeleteRoleCommand(long Id) : IBaseCommand;

public class DeleteRoleCommandHandler : IBaseCommandHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetTracking(request.Id);
        if (role == null)
            return OperationResult.NotFound();

        await _roleRepository.RemoveRole(role);
        await _roleRepository.Save();
        return OperationResult.Success();
    }
}