using Common.Application;

namespace Shop.Application.Users.AddToken;

public record AddUserTokenCommand(
    long UserId,
    string HashJwtToken,
    string HashRefreshToken,
    DateTime TokenExpireDate,
    DateTime RefreshTokenExpireDate,
    string Device
    ) : IBaseCommand;