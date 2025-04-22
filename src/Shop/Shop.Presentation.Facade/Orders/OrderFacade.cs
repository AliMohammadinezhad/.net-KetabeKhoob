using Common.Application;
using MediatR;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetCurrent;

namespace Shop.Presentation.Facade.Orders;

internal class OrderFacade : IOrderFacade
{
    private readonly IMediator _mediator;

    public OrderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddOrderItem(AddOrderItemCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> CheckOutOrder(CheckoutOrderCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> DecreaseOrderItemCount(DecreaseOrderItemCountCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> IncreaseOrderItemCount(IncreaseOrderItemCountCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> RemoveOrderItem(RemoveOrderItemCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OrderDto?> GetOrderById(long id, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
    }

    public async Task<OrderFilterResult> GetOrderByFilter(OrderFilterParams filterParams, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetOrdersByFilterQuery(filterParams), cancellationToken);
    }

    public async Task<OrderDto?> GetCurrentOrder(long userId)
    {
        return await _mediator.Send(new GetCurrentUserOrderQuery(userId));
    }
}