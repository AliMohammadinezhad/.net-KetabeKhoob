using System.Runtime.InteropServices.ComTypes;
using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.ChangeStatus;
using Shop.Application.Sellers.EditInventory;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Sellers.Inventories;

namespace Shop.Api.Controllers;

public class InventoryController : ApiController
{
    private readonly ISellerInventoryFacade _sellerInventoryFacade;

    public InventoryController(ISellerInventoryFacade sellerInventoryFacade)
    {
        _sellerInventoryFacade = sellerInventoryFacade;
    }

    [PermissionChecker(Permission.AddInventory)]
    [HttpPost]
    public async Task<ApiResult> AddInventory(AddSellerInventoryCommand command)
    {
        var result = await _sellerInventoryFacade.AddSellerInventory(command);
        return CommandResult(result);
    }

    [HttpPut("ChangeStatus")]
    [PermissionChecker(Permission.ChangeStatusInventory)]
    public async Task<ApiResult> ChangeInventoryStatus(ChangeSellerInventoryStatusCommand command)
    {
        var result = await _sellerInventoryFacade.ChangeSellerInventoryStatus(command);
        return CommandResult(result);
    }

    [HttpPut]
    [PermissionChecker(Permission.EditInventory)]
    public async Task<ApiResult> EditInventory(EditSellerInventoryCommand command)
    {
        var result = await _sellerInventoryFacade.EditSellerInventory(command);
        return CommandResult(result);
    }


}