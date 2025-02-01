using Common.Application;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.ChangeStatus;
using Shop.Application.Sellers.EditInventory;

namespace Shop.Presentation.Facade.Sellers.Inventories;

public interface ISellerInventoryFacade
{
    Task<OperationResult> EditSellerInventory(EditSellerInventoryCommand command,
        CancellationToken cancellationToken = default);

    Task<OperationResult> AddSellerInventory(AddSellerInventoryCommand command,
        CancellationToken cancellationToken = default);

    Task<OperationResult> ChangeSellerInventoryStatus(ChangeSellerInventoryStatusCommand command,
        CancellationToken cancellationToken = default);
}