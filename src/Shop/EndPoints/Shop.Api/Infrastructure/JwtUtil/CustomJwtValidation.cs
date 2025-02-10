using Common.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shop.Presentation.Facade.Users;

namespace Shop.Api.Infrastructure.JwtUtil;

public class CustomJwtValidation
{
    private readonly IUserFacade _userFacade;

    public CustomJwtValidation(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    public async Task Validate(TokenValidatedContext context)
    {
        var userId = context.Principal.GetUserId();
        var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var tokenInDb = await _userFacade.GetUserTokenByAccessToken(accessToken);
        if (tokenInDb is null)
            context.Fail("Token Not Found");

        var user = await _userFacade.GetUserById(userId);
        if (user is null or { IsActive: false })
            context.Fail("User Is InActive");
    }
}