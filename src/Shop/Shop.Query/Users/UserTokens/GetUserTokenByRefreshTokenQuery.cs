using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens;

public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IQuery<UserTokenDto?>;