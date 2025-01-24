using Common.Application;
using Shop.Domain.UserAgg.Enums;

namespace Shop.Application.Users.ChargeWallet;

public record ChargeUserWalletCommand(
    long UserId,
    int Price,
    string Description,
    bool IsFinally,
    WalletType Type
    ) : IBaseCommand;