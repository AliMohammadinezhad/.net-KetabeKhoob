using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg.Enums;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetCurrent;

public class GetCurrentUserOrderQueryHandler : IQueryHandler<GetCurrentUserOrderQuery, OrderDto?>
{
    private readonly ShopContext _context;
    private readonly DapperContext _dapperContext;

    public GetCurrentUserOrderQueryHandler(ShopContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto?> Handle(GetCurrentUserOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == request.userId && x.Status == OrderStatus.Pending,
                cancellationToken);
        if (order is null) return null;

        var orderDto = order.Map();

        orderDto.UserFullName = await _context.Users
            .Where(x => x.Id == orderDto.UserId)
            .Select(x => $"{x.Name} {x.Family}")
            .FirstOrDefaultAsync(cancellationToken);

        orderDto.Items = await orderDto.GetOrderItem(_dapperContext);

        return orderDto;
    }
}