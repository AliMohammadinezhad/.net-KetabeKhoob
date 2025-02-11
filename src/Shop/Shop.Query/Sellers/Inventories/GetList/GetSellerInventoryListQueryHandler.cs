using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetList;

public class GetSellerInventoryListQueryHandler : IQueryHandler<GetSellerInventoryListQuery, List<InventoryDto?>>
{
    private readonly DapperContext _dapperContext;

    public GetSellerInventoryListQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<InventoryDto?>> Handle(GetSellerInventoryListQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"""
                   SELECT i.Id, i.SellerId, i.ProductId, i.Count, i.Price, i.CreationDate, DiscountPercentage,
                   s.ShopName, p.Title as ProductTitle, p.ImageName as ProductImageName
                   FROM {_dapperContext.Inventories} i
                   INNER JOIN {_dapperContext.Sellers} s
                   ON i.SellerId = s.Id
                   INNER JOIN {_dapperContext.Products} p
                   ON i.ProductId = p.Id
                   WHERE i.SellerId = @id
                   """;

        var inventories = await connection.QueryAsync<InventoryDto>(sql, new { id = request.SellerId });
        return inventories.ToList()!;
    }
}