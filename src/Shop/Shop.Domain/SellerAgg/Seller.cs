﻿using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SellerAgg.Enums;

namespace Shop.Domain.SellerAgg;

public class Seller : AggregateRoot
{
    public long UserId { get; private set; }
    public string ShopName { get; private set; }
    public string NationalCode { get; private set; }
    public SellerStatus Status { get; private set; }
    public DateTime? LastUpdate { get; private set; }
    public List<SellerInventory> Inventories { get; private set; }

    private Seller()
    {
    }

    public Seller(long userId, string shopName, string nationalCode)
    {
        Guard(shopName, nationalCode);
        UserId = userId;
        ShopName = shopName;
        NationalCode = nationalCode;
        Inventories = new List<SellerInventory>();
    }

    public void ChangeStatus(SellerStatus status)
    {
        Status = status;
        LastUpdate = DateTime.Now;
    }

    public void Edit(string shopName, string nationalCode)
    {
        Guard(shopName, nationalCode);
        ShopName = shopName;
        NationalCode = nationalCode;
    }

    public void AddInventory(SellerInventory inventory)
    {
        if (Inventories.Any(p => p.ProductId == inventory.ProductId))
            throw new InvalidDomainDataException("این محصول قبلا ثبت شده است.");

        Inventories.Add(inventory);
    }

    public void EditInventory(SellerInventory inventory)
    {
        var currentInventory = Inventories.FirstOrDefault(p => p.Id == inventory.Id);
        if (currentInventory is null)
            return;
        Inventories.Remove(currentInventory);
        Inventories.Add(inventory);
    }

    public void DeleteInventory(long inventoryId)
    {
        var currentInventory = Inventories.FirstOrDefault(p => p.Id == inventoryId);
        if (currentInventory is null)
            throw new NullOrEmptyDomainDataException("محصول یافت نشد.");

        Inventories.Remove(currentInventory);
    }

    private void Guard(string shopName, string nationalCode)
    {
        NullOrEmptyDomainDataException.CheckString(shopName, nameof(shopName));
        NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));
        if (!IranianNationalIdChecker.IsValid(nationalCode))
            throw new InvalidDomainDataException("کد ملی نامعتبر است.");
    }

}