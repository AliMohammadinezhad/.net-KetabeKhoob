using Common.Query;

namespace Shop.Query.Orders.DTOs;

public class ShippingMethodDto
{
    public string ShippingType { get; set; }
    public int ShippingCost { get; set; }
}