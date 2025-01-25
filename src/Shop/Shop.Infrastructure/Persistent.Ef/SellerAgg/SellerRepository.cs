using Dapper;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    private readonly DapperContext _context;
    public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _context = dapperContext;
    }

    public async Task<InventoryResult?> GetInventoryById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = $"SELECT * FROM {_context.Inventories} WHERE Id = @id";
        var result = await connection.QueryFirstOrDefaultAsync<InventoryResult>(sql, new { id = id });
        return result;
    }
}