using Common.Application;
using FluentValidation;
using Shop.Domain.SellerAgg.Enums;

namespace Shop.Application.Sellers.ChangeStatus;

public record ChangeSellerInventoryStatusCommand(
    long SellerId,
    SellerStatus Status
    ) : IBaseCommand;