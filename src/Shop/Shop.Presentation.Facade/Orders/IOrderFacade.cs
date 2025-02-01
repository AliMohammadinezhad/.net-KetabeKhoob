using Common.Application;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Query.Orders.DTOs;

namespace Shop.Presentation.Facade.Orders;

public interface IOrderFacade
{
    Task<OperationResult> AddOrderItem(AddOrderItemCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> CheckOutOrder(CheckoutOrderCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> DecreaseOrderItemCount(DecreaseOrderItemCountCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> IncreaseOrderItemCount(IncreaseOrderItemCountCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> RemoveOrderItem(RemoveOrderItemCommand command, CancellationToken cancellationToken = default);
    
    Task<OrderDto?> GetOrderById(long id, CancellationToken cancellationToken = default);
    Task<OrderFilterResult> GetOrderByFilter(OrderFilterParams filterParams, CancellationToken cancellationToken = default);

}