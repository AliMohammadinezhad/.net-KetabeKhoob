using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers;

public class SellerDomainService : ISellerDomainService
{
    private readonly ISellerRepository _sellerRepository;

    public SellerDomainService(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public bool CheckSellerInformation(Seller seller)
    {
        var sellerExists = _sellerRepository.Exists(x => x.NationalCode == seller.NationalCode || x.UserId == seller.UserId);
        return !sellerExists;
    }

    public bool NationalCodeExistInDataBase(string nationalCode)
    {
         return _sellerRepository.Exists(x => x.NationalCode == nationalCode);
    }
}