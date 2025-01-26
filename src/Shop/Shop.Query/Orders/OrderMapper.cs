using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
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
            Items = new(),
            Id = order.Id,
            Status = order.Status,
            Address = order.Address,
            Discount = order.Discount,
            LastUpdate = order.LastUpdate,
            ShippingMethod = order.ShippingMethod,
            UserFullName = "",
            UserId = order.UserId,
        };
    }

    public static async Task<List<OrderItemDto>> GetOrderItem(this OrderDto orderDto, DapperContext context)
    {
        var model = new List<OrderItemDto>();
        using var connection = context.CreateConnection();
        var sql = $"""
                   SELECT s.ShopName, o.OrderId, o.InventoryId, o.Count, o.Price,
                   p.Title as [Product.Title], p.Slug as [Product.Slug],
                   p.ImageName as [Product.ImageName]
                   FROM {context.OrderItems} o
                   INNER JOIN {context.Inventories} i ON o.InventoryId=i.Id
                   INNER JOIN {context.Products} p ON i.ProductId=p.Id
                   INNER JOIN {context.Sellers} s ON i.SellerId=s.Id
                   WHERE o.OrderId=@orderId
                   """;
        var result = await connection.QueryAsync<OrderItemDto>(sql, new{orderId=orderDto.Id});
        return result.ToList();
    }
    public static OrderFilterData? MapFilterData(this Order? order, ShopContext _context)
    {
        if (order == null) return null;

        var userFullName = _context.Users
            .Where(x => x.Id == order.UserId)
            .Select(x => $"{x.Name} {x.Family}")
            .First();
        
        return new OrderFilterData()
        {
            CreationDate = order.CreationDate,
            Id = order.Id,
            Status = order.Status,
            UserFullName = userFullName,
            UserId = order.UserId,
            City = order.Address?.City,
            Province = order.Address?.Province,
            ShippingType = order.ShippingMethod?.ShippingType,
            TotalItemCount = order.ItemCount,
            TotalPrice = order.TotalPrice
        };
    }
}