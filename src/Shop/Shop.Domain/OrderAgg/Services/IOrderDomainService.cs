namespace Shop.Domain.OrderAgg.Services;

public interface IOrderDomainService
{
    bool IsOrderItemQuantityAvailable(int requestedQuantity, long inventoryId);
}