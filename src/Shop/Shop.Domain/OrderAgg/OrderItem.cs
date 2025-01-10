using Common.Domain;
using Common.Domain.Exceptions;

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
    public OrderItem(long inventoryId, int count, int price)
    {
        PriceGuard(price);
        CountGuard(count);
        InventoryId = inventoryId;
        Count = count;
        Price = price;
    }

    public void ChangeCount(int newCount)
    {
        CountGuard(newCount);
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


    private void CountGuard(int newCount)
    {
        if (newCount < 1)
            throw new InvalidDomainDataException("تعداد کالا نامعتبر است.");
    }
}