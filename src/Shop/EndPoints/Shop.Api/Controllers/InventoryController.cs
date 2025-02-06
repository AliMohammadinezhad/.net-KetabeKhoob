using System.Runtime.InteropServices.ComTypes;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Presentation.Facade.Sellers.Inventories;

namespace Shop.Api.Controllers;

public class InventoryController : ApiController
{
    private readonly ISellerInventoryFacade _sellerInventoryFacade;

    public InventoryController(ISellerInventoryFacade sellerInventoryFacade)
    {
        _sellerInventoryFacade = sellerInventoryFacade;
    }

    // TODO: complete inventory controller
    
}