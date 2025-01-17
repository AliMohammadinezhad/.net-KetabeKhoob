using Common.Application;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.OrderAgg.Services;

namespace Shop.Application.Orders.IncreaseItemCount;

public class IncreaseOrderItemCountCommandHandler : IBaseCommandHandler<IncreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDomainService _orderDomainService;

    public IncreaseOrderItemCountCommandHandler(IOrderRepository orderRepository, IOrderDomainService orderDomainService)
    {
        _orderRepository = orderRepository;
        _orderDomainService = orderDomainService;
    }

    public async Task<OperationResult> Handle(IncreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentUserOrderById(request.UserId);
        if(currentOrder is null)
            return OperationResult.NotFound();

        currentOrder.IncreaseItemCount(request.ItemId, request.Count, _orderDomainService);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}