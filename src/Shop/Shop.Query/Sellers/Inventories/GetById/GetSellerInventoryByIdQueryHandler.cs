using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetById;

public class GetSellerInventoryByIdQueryHandler : IQueryHandler<GetSellerInventoryByIdQuery, InventoryDto?>
{
    private readonly DapperContext _dapperContext;

    public GetSellerInventoryByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<InventoryDto?> Handle(GetSellerInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"""
                   SELECT Top(1) i.Id, i.SellerId, i.ProductId, i.Count, i.Price, i.CreationDate, DiscountPercentage,
                   s.ShopName, p.Title as ProductTitle, p.ImageName as ProductImageName
                   FROM {_dapperContext.Inventories} i
                   INNER JOIN {_dapperContext.Sellers} s
                   ON i.SellerId = s.Id
                   INNER JOIN {_dapperContext.Products} p
                   ON i.ProductId = p.Id
                   WHERE i.Id = @id
                   """;

        var inventory = await connection.QueryFirstOrDefaultAsync<InventoryDto>(sql, new { id = request.InventoryId });
        return inventory ?? null;
    }
}