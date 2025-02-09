using Common.Application;

namespace Shop.Application.Users.RemoveToken;

public record RemoveUserTokenCommand(long TokenId, long UserId) : IBaseCommand;