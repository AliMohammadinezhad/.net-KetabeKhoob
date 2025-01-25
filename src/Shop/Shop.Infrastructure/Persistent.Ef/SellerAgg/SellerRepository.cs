using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    private readonly ShopContext _context;
    public SellerRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<InventoryResult> GetInventoryById(long id)
    {
        throw new NotImplementedException(); // TODO: Implement Inventory with Dapper
    }
}