using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.OrderAgg.Services;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Orders;

public class OrderDomainService : IOrderDomainService
{
    private readonly ISellerRepository _sellerRepository;

    public OrderDomainService(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public bool IsOrderItemQuantityAvailable(int requestedQuantity, long inventoryId)
    {
        var inventory = _sellerRepository.GetInventoryById(inventoryId).Result;
        if (inventory is null)
            throw new ApplicationException("انبار موجود نیست.");

        return requestedQuantity > inventory.Count;
    }
}