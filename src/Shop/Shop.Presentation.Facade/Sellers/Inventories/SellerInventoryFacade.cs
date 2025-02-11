using Common.Application;
using MediatR;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.ChangeStatus;
using Shop.Application.Sellers.EditInventory;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.Inventories.GetById;
using Shop.Query.Sellers.Inventories.GetList;

namespace Shop.Presentation.Facade.Sellers.Inventories;

internal class SellerInventoryFacade : ISellerInventoryFacade
{
    private readonly IMediator _mediator;

    public SellerInventoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> EditSellerInventory(EditSellerInventoryCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> AddSellerInventory(AddSellerInventoryCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> ChangeSellerInventoryStatus(ChangeSellerInventoryStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<InventoryDto?> GetSellerInventoryById(long inventoryId)
    {
        return await _mediator.Send(new GetSellerInventoryByIdQuery(inventoryId));
    }

    public async Task<List<InventoryDto?>> GetSellerInventoryList(long sellerId)
    {
        return await _mediator.Send(new GetSellerInventoryListQuery(sellerId));
    }
}