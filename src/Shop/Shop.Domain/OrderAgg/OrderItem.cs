using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.Services;

namespace Shop.Domain.OrderAgg;

public class OrderItem : BaseEntity
{
    public long OrderId { get; private set; }
    public long InventoryId { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public int TotalPrice => Price * Count;

    private OrderItem()
    {
        
    }
    public OrderItem(long inventoryId, int count, int price, IOrderDomainService domainService)
    {
        PriceGuard(price);
        CountGuard(count, domainService);
        InventoryId = inventoryId;
        Count = count;
        Price = price;
    }

    public void IncreaseCount(int count, IOrderDomainService domainService)
    {
        if(domainService.IsWantedOrderItemCountExistInInventory(InventoryId, count))
            Count += count;
    }

    public void DecreaseCount(int count)
    {
        if (Count == 1)
            return;
        if ((Count - count) <= 0)
            return;
        Count -= count;
    }

    public void ChangeCount(int newCount, IOrderDomainService domainService)
    {
        CountGuard(newCount, domainService);
        Count = newCount;
    }

    public void SetPrice(int newPrice)
    {
        PriceGuard(newPrice);
        Price = newPrice;
    }

    private void PriceGuard(int newPrice)
    {
        if (newPrice < 1)
            throw new InvalidDomainDataException("مبلغ کالا نامعتبر است.");
    }


    private void CountGuard(int newCount, IOrderDomainService domainService)
    {
        if (newCount < 1)
            throw new InvalidDomainDataException("تعداد کالا نامعتبر است.");

        if (domainService.IsWantedOrderItemCountExistInInventory(InventoryId, newCount))
            throw new InvalidDomainDataException("تعداد کالای خواسته شده در انبار موجود نیست.");

    }
}