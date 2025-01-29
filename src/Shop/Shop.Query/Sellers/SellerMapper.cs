using System.ComponentModel;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Enums;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers;

public static class SellerMapper
{
    public static SellerDto? MapNullable(this Seller? seller)
    {
        return seller?.Map();
    }

    public static SellerDto Map(this Seller seller)
    {
        return new SellerDto
        {
            Id = seller.Id,
            CreationDate = seller.CreationDate,
            NationalCode = seller.NationalCode,
            ShopName = seller.ShopName,
            Status = MapSellerStatus(seller.Status),
            UserId = seller.UserId
        };
    }

    private static SellerStatusDto MapSellerStatus(SellerStatus status)
    {
        return status switch
        {
            SellerStatus.New => SellerStatusDto.New,
            SellerStatus.Accepted => SellerStatusDto.Accepted,
            SellerStatus.InActive => SellerStatusDto.InActive,
            SellerStatus.Rejected => SellerStatusDto.Rejected,
            _ => throw new InvalidEnumArgumentException(),
        };
    }
}