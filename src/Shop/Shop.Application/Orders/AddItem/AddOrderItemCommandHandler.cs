using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Orders.AddItem;

public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISellerRepository _sellerRepository;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerRepository)
    {
        _orderRepository = orderRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _sellerRepository.GetInventoryById(request.InventoryId);
        if (inventory is null)
            return OperationResult.NotFound();

        if (inventory.Count < request.Count)
            return OperationResult.Error("تعداد محصولات درخواستی بیشتر از موجودی میباشد.");

        var order = await _orderRepository.GetCurrentUserOrderById(request.UserId) ?? new Order(request.UserId);

        order.AddItem(new OrderItem(request.InventoryId, request.Count, inventory.Price));
        
        if(ItemCountBiggerThanInventoryCount(inventory, order))
            return OperationResult.Error("تعداد محصولات درخواستی بیشتر از موجودی میباشد.");
        
        await _orderRepository.Save();
        return OperationResult.Success();
    }

    private bool ItemCountBiggerThanInventoryCount(InventoryResult inventory, Order order)
    {
        var orderItem = order.Items.FirstOrDefault(x => x.InventoryId == inventory.Id);
        return orderItem?.Count  > inventory.Count;
    }
}