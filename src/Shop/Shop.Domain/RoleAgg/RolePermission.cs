using Common.Domain;
using Shop.Domain.RoleAgg.Enums;

namespace Shop.Domain.RoleAgg;

public class RolePermission : BaseEntity
{
    public long RoleId { get; private set; }
    public Permission Permission { get; private set; }

    private RolePermission()
    {
    }

    private RolePermission(Permission permission)
    {
        Permission = permission;
    }

    public static RolePermission CreateRolePermission()
    {
        return new RolePermission();
    }

    public static RolePermission CreateRolePermission(Permission permission)
    {
        return new RolePermission(permission);
    }
}