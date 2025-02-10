using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Sellers;
using Shop.Query.Sellers.DTOs;

namespace Shop.Api.Controllers;

public class SellerController : ApiController
{
    private readonly ISellerFacade _sellerFacade;

    public SellerController(ISellerFacade sellerFacade)
    {
        _sellerFacade = sellerFacade;
    }

    [HttpGet("{sellerId:long}")]
    public async Task<ApiResult<SellerDto?>> GetSellerById(long sellerId)
    {
        var result = await _sellerFacade.GetSellerById(sellerId);
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
}