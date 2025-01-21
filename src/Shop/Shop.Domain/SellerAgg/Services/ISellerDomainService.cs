namespace Shop.Domain.SellerAgg.Services;

public interface ISellerDomainService
{
    bool CheckSellerInformation(Seller seller);
    bool NationalCodeExistInDataBase(string nationalCode);
}