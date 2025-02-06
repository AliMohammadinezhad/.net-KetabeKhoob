using Microsoft.IdentityModel.Tokens;
using Shop.Domain.UserAgg;
using Shop.Query.Users.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.Api.Infrastructure.JwtUtil;

public class JwtTokenBuilder
{
    public static string buildToken(UserDto userDto, IConfiguration configuration)
    {
        var roles = userDto.UserRoles.Select(s => s.RoleTitle);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.MobilePhone, userDto.PhoneNumber),
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new Claim(ClaimTypes.Role,string.Join("-",roles))
        };
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:Issuer"],
            audience: configuration["JwtConfig:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}