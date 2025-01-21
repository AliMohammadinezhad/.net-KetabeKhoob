using Common.Application;

namespace Shop.Application.Sellers.Edit;

public record EditSellerCommand(
    long UserId,
    string ShopName,
    string NationalCode) : IBaseCommand;