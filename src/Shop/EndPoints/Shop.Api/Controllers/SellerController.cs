using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.ChangeStatus;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Sellers.DTOs;

namespace Shop.Api.Controllers;

public class SellerController : ApiController
{
    private readonly ISellerFacade _sellerFacade;
    private readonly ISellerInventoryFacade _sellerInventoryFacade;

    public SellerController(ISellerFacade sellerFacade, ISellerInventoryFacade sellerInventoryFacade)
    {
        _sellerFacade = sellerFacade;
        _sellerInventoryFacade = sellerInventoryFacade;
    }

    [HttpGet("{sellerId:long}")]
    public async Task<ApiResult<SellerDto?>> GetSellerById(long sellerId)
    {
        var result = await _sellerFacade.GetSellerById(sellerId);
        return QueryResult(result);
    }

    [Authorize]
    [HttpGet("Current")]
    public async Task<ApiResult<SellerDto?>> GetSellerByUserId()
    {
        var result = await _sellerFacade.GetSellerByUserId(User.GetUserId());
        return QueryResult(result);
    }

    [PermissionChecker(Permission.SellerManagement)]
    [HttpGet]
    public async Task<ApiResult<SellerFilterResult?>> GetSellerByFilter([FromQuery] SellerFilterParams filterParams)
    {
        var result = await _sellerFacade.GetSellerByFilter(filterParams);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.SellerManagement)]
    [HttpPost]
    public async Task<ApiResult> CreateSeller(CreateSellerCommand command)
    {
        var result = await _sellerFacade.CreateSeller(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.SellerManagement)]
    [HttpPut]
    public async Task<ApiResult> EditSeller(EditSellerCommand command)
    {
        var result = await _sellerFacade.EditSeller(command);
        return CommandResult(result);
    }

    [HttpGet("Inventory")]
    [PermissionChecker(Permission.SellerPanel)]
    public async Task<ApiResult<List<InventoryDto?>>> GetInventories()
    {
        var seller = await _sellerFacade.GetSellerByUserId(User.GetUserId());
        if (seller is null)
            return QueryResult(new List<InventoryDto?>());

        var result = await _sellerInventoryFacade.GetSellerInventoryList(seller.Id);
        return QueryResult(result);
    }

    [HttpGet("Inventory/{inventoryId}")]
    [PermissionChecker(Permission.SellerPanel)]
    public async Task<ApiResult<InventoryDto?>> GetInventoryById(long inventoryId)
    {
        var seller = await _sellerFacade.GetSellerByUserId(User.GetUserId());
        if (seller is null)
            return QueryResult(new InventoryDto());

        var result = await _sellerInventoryFacade.GetSellerInventoryById(inventoryId);
        if(result is null || result.SellerId != seller.Id)
            return QueryResult(new InventoryDto());

        return QueryResult(result);
    }

    [PermissionChecker(Permission.AddInventory)]
    [HttpPost("Inventory")]
    public async Task<ApiResult> AddInventory(AddSellerInventoryCommand command)
    {
        var result = await _sellerInventoryFacade.AddSellerInventory(command);
        return CommandResult(result);
    }

    [HttpPut("Inventory/ChangeStatus")]
    [PermissionChecker(Permission.ChangeStatusInventory)]
    public async Task<ApiResult> ChangeInventoryStatus(ChangeSellerInventoryStatusCommand command)
    {
        var result = await _sellerInventoryFacade.ChangeSellerInventoryStatus(command);
        return CommandResult(result);
    }

    [HttpPut("Inventory")]
    [PermissionChecker(Permission.EditInventory)]
    public async Task<ApiResult> EditInventory(EditSellerInventoryCommand command)
    {
        var result = await _sellerInventoryFacade.EditSellerInventory(command);
        return CommandResult(result);
    }
}