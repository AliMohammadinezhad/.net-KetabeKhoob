﻿using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.ValueObjects;

namespace Shop.Domain.OrderAgg;

public class Order : AggregateRoot
{
    public long UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderAddress? Address { get; private set; }
    public OrderDiscount? Discount { get; private set; }
    public ShippingMethod? ShippingMethod { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public DateTime LastUpdate { get; private set; }

    public int TotalPrice
    {
        get
        {
            var totalPrice = Items.Sum(f => f.TotalPrice);
            if (ShippingMethod is not null)
                totalPrice += ShippingMethod.ShippingCost;
            if (Discount is not null)
                totalPrice -= Discount.DiscountAmount;
            return totalPrice;
        }

    }

    public int ItemCount => Items.Count;

    private Order()
    {
    }

    public Order(long userId)
    {
        UserId = userId;
        Status = OrderStatus.Pending;
        Items = new List<OrderItem>();
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }

    public void RemoveItem(long itemId)
    {
        var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
        if(currentItem is not null) 
            Items.Remove(currentItem);
    }

    public void ChangeCountItem(long itemId, int newCount)
    {
        var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
        if (currentItem is null)
            throw new NullOrEmptyDomainDataException();

        currentItem.ChangeCount(newCount);
    }

    public void ChangeStatus(OrderStatus status)
    {
        Status = status;
        LastUpdate = DateTime.Now;
    }

    public void CheckOut(OrderAddress address)
    {
        Address = address;

    }
}