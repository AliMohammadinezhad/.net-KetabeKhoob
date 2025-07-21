using Microsoft.EntityFrameworkCore;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.RoleAgg;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    private readonly ShopContext _context;
    public RoleRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> RemoveRole(Role role)
    {
        if (role == null)
            return false;

        _context.Roles.Remove(role);
        return true;
    }
}