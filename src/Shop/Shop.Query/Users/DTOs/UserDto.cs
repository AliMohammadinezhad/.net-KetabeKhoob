using Shop.Domain.UserAgg;

namespace Shop.Query.Users.DTOs;

public class UserDto
{
    public string Name { get;  set; }
    public string Family { get;  set; }
    public string PhoneNumber { get;  set; }
    public string Email { get;  set; }
    public string Password { get;  set; }
    public string AvatarName { get;  set; }
    public GenderDto Gender { get;  set; }
    public List<UserRoleDto> UserRoles { get;  set; }
}