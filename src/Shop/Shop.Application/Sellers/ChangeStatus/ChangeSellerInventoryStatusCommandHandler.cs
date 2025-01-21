using Common.Application;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Sellers.ChangeStatus;

public class ChangeSellerInventoryStatusCommandHandler : IBaseCommandHandler<ChangeSellerInventoryStatusCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public ChangeSellerInventoryStatusCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(ChangeSellerInventoryStatusCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.SellerId);
        if (seller is null)
            return OperationResult.NotFound();

        seller.ChangeStatus(request.Status);
        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}