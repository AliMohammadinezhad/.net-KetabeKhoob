using Common.Application;
using Shop.Application.Sellers.Create;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Query.Sellers.DTOs;
using Shop.Query.SiteEntities.Banners.DTOs;

namespace Shop.Presentation.Facade.SiteEntities.Banners;

public interface IBannerFacade
{
    Task<OperationResult> CreateBanner(CreateBannerCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> EditBanner(EditBannerCommand command, CancellationToken cancellationToken = default);

    Task<BannerDto?> GetBannerById(long id, CancellationToken cancellationToken = default);
    Task<List<BannerDto?>> GetBannerList(CancellationToken cancellationToken = default);
}