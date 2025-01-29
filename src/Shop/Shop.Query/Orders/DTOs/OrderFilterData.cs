using Common.Query;
using Shop.Domain.OrderAgg.Enums;

namespace Shop.Query.Orders.DTOs;

public class OrderFilterData : BaseDto
{
    public long UserId { get; set; }
    public string UserFullName { get; set; }
    public OrderStatusDto Status { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? ShippingType { get; set; }
    public int TotalPrice { get; set; }
    public int TotalItemCount { get; set; }
}