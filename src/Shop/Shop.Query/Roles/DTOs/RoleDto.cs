using Common.Query;

namespace Shop.Query.Roles.DTOs;

public class RoleDto : BaseDto
{
    public string Title { get; set; }
    public List<RolePermissionDto> Permissions { get; set; }
}