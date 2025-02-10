using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetByAccessToken;

public class GetUserTokenByAccessTokenQueryHandler : IQueryHandler<GetUserTokenByAccessTokenQuery, UserTokenDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserTokenByAccessTokenQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserTokenDto?> Handle(GetUserTokenByAccessTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"""
                   SELECT TOP(1) *
                   FROM {_dapperContext.UserTokens}
                   WHERE HashJwtToken=@hashAccessToken
                   """;
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto?>(sql, new { hashAccessToken = request.HashAccessToken });
    }
}