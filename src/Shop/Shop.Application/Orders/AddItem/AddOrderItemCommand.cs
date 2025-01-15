using Common.Application;

namespace Shop.Application.Orders.AddItem;

public record AddOrderItemCommand(long InventoryId, int Count, long UserId) : IBaseCommand;