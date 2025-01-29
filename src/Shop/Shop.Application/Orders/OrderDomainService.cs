using Shop.Domain.OrderAgg.Services;

namespace Shop.Application.Orders;

public class OrderDomainService : IOrderDomainService
{
    public bool IsWantedOrderItemCountExistInInventory(long inventoryId, int count)
    {
        throw new NotImplementedException();
    }

    public bool IsOrderItemExistInInventory(long inventoryId)
    {
        throw new NotImplementedException();
    }
}