using Common.Query;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetList;

public record GetRoleListQuery : IQuery<List<RoleDto>>;