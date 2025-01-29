using Common.Query;

namespace Shop.Query.Orders.DTOs;

public class OrderDiscountDto
{
    public string DiscountTitle { get; set; }
    public int DiscountAmount { get; set; }
}