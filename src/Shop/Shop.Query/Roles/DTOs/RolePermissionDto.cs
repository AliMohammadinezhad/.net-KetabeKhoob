using Common.Query;

namespace Shop.Query.Roles.DTOs;

public class RolePermissionDto : BaseDto
{
    public long RoleId { get; set; }
    public PermissionDto Permission { get;  set; }
}