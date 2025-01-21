using Common.Application;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Sellers.AddInventory;

public class AddSellerInventoryCommandHandler : IBaseCommandHandler<AddSellerInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public AddSellerInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(AddSellerInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.SellerId);
        if (seller is null)
            return OperationResult.NotFound();

        var inventory = new SellerInventory(request.ProductId, request.Count, request.Price, request.DiscountPercentage);
        seller.AddInventory(inventory);

        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}