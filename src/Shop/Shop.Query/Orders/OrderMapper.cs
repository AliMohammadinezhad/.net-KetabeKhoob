using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.ValueObjects;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders;

internal static class OrderMapper
{
    public static OrderDto Map(this Order order)
    {
        return new OrderDto()
        {
            CreationDate = order.CreationDate,
            Items = [],
            Id = order.Id,
            Status = MapOrderStatus(order.Status),
            Address = MapOrderAddress(order.Address),
            Discount = MapOrderDiscount(order.Discount),
            LastUpdate = order.LastUpdate,
            ShippingMethod = MapOrderShippingMethod(order.ShippingMethod),
            UserFullName = "",
            UserId = order.UserId,
        };
    }

    private static ShippingMethodDto? MapOrderShippingMethod(ShippingMethod? shippingMethod)
    {
        if (shippingMethod is null) return null;
        return new ShippingMethodDto()
        {
            ShippingCost = shippingMethod.ShippingCost,
            ShippingType = shippingMethod.ShippingType,
        };
    }

    private static OrderDiscountDto? MapOrderDiscount(OrderDiscount? discount)
    {
        if (discount is null) return null;
        return new OrderDiscountDto
        {
            DiscountTitle = discount.DiscountTitle,
            DiscountAmount = discount.DiscountAmount
        };
    }

    private static OrderAddressDto? MapOrderAddress(OrderAddress? address)
    {
        if (address is null) return null;
        var result = new OrderAddressDto()
        {
            City = address.City,
            CreationDate = address.CreationDate,
            Family = address.Family,
            Id = address.Id,
            Name = address.Name,
            NationalCode = address.NationalCode,
            OrderId = address.OrderId,
            PhoneNumber = address.PhoneNumber,
            PostalAddress = address.PostalAddress,
            PostalCode = address.PostalCode,
            Province = address.Province
        };
        return result;
    }

    private static OrderStatusDto MapOrderStatus(OrderStatus orderStatus)
    {
        return orderStatus switch
        {
            OrderStatus.Pending => OrderStatusDto.Pending,
            OrderStatus.Finally => OrderStatusDto.Finally,
            OrderStatus.Shipping => OrderStatusDto.Shipping,
            OrderStatus.Rejected => OrderStatusDto.Rejected,
            _ => throw new ArgumentOutOfRangeException(nameof(orderStatus), orderStatus, "Unknown Status")
        };
    }

    public static async Task<List<OrderItemDto>> GetOrderItem(this OrderDto orderDto, DapperContext context)
    {
        var model = new List<OrderItemDto>();
        using var connection = context.CreateConnection();
        var sql = $"""
                   SELECT s.ShopName, o.OrderId, o.InventoryId, o.Count, o.Price,
                   p.Title, p.Slug, p.ImageName
                   FROM {context.OrderItems} o
                   INNER JOIN {context.Inventories} i ON o.InventoryId=i.Id
                   INNER JOIN {context.Products} p ON i.ProductId=p.Id
                   INNER JOIN {context.Sellers} s ON i.SellerId=s.Id
                   WHERE o.OrderId=@orderId
                   """;
        var result = await connection
            .QueryAsync<OrderItemDto, ProductOrderItem, OrderItemDto>(sql, (orderItem, product) =>
                {
                    orderItem.Product = product; // Assign the nested Product property
                    return orderItem;
                },
                new { orderId = orderDto.Id },
                splitOn: "Title"
                );
        return result.ToList();
    }
    public static OrderFilterData MapFilterData(this Order order, ShopContext _context)
    {
        var userFullName = _context.Users
            .Where(x => x.Id == order.UserId)
            .Select(x => $"{x.Name} {x.Family}")
            .FirstOrDefault();
        
        return new OrderFilterData()
        {
            CreationDate = order.CreationDate,
            Id = order.Id,
            Status = MapOrderStatus(order.Status),
            UserFullName = userFullName ?? "",
            UserId = order.UserId,
            City = order.Address?.City,
            Province = order.Address?.Province,
            ShippingType = order.ShippingMethod?.ShippingType,
            TotalItemCount = order.ItemCount,
            TotalPrice = order.TotalPrice
        };
    }
}