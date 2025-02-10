using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetByAccessToken;

public record GetUserTokenByAccessTokenQuery(string HashAccessToken) : IQuery<UserTokenDto?>;