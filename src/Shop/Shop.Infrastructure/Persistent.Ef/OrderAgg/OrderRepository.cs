using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.OrderAgg;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly ShopContext _context;
    public OrderRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Order?> GetCurrentUserOrderById(long userId)
    {
        return await _context.Orders.FindAsync(userId);
    }
}