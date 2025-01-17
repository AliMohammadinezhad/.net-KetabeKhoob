namespace Shop.Domain.OrderAgg.Services;

public interface IOrderDomainService
{
    public bool IsWantedOrderItemCountExistInInventory(long inventoryId, int count);
    public bool IsOrderItemExistInInventory(long inventoryId);
}