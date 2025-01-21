using Common.Application;
using FluentValidation;

namespace Shop.Application.Sellers.AddInventory;

public record AddSellerInventoryCommand(
    long SellerId,
    long ProductId,
    int Count,
    int Price,
    int? DiscountPercentage = null
) : IBaseCommand;