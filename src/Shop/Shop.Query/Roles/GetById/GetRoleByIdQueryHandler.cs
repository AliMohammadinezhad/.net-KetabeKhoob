using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetById;

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly ShopContext _context;

    public GetRoleByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken);

        return role.Map();
    }
}