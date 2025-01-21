using Common.Application;
using Shop.Domain.SellerAgg.Enums;

namespace Shop.Application.Sellers.EditInventory;

public record EditSellerInventoryCommand(
    long SellerId,
    long InventoryId,
    int Count,
    int Price,
    int? DiscountPercentage = null
    ) : IBaseCommand;