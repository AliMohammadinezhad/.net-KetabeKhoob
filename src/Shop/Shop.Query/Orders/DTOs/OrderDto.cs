using Common.Query;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.ValueObjects;

namespace Shop.Query.Orders.DTOs;

public class OrderDto : BaseDto
{
    public long UserId { get;  set; }
    public string? UserFullName { get; set; }
    public OrderStatusDto Status { get;  set; }
    public OrderAddressDto? Address { get;  set; }
    public OrderDiscountDto? Discount { get;  set; }
    public ShippingMethodDto? ShippingMethod { get;  set; }
    public List<OrderItemDto> Items { get;  set; }
    public DateTime LastUpdate { get;  set; }
}