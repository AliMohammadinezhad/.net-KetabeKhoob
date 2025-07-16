using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Delete;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Query.SiteEntities.Banners.DTOs;
using Shop.Query.SiteEntities.Banners.GetById;
using Shop.Query.SiteEntities.Banners.GetList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.SiteEntities.Banners;

internal class BannerFacade : IBannerFacade
{
    private readonly IMediator _mediator;

    public BannerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateBanner(CreateBannerCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> EditBanner(EditBannerCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> DeleteBanner(long bannerId, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new DeleteBannerCommand(bannerId), cancellationToken);
    }

    public async Task<BannerDto?> GetBannerById(long id, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetBannerByIdQuery(id), cancellationToken);
    }

    public async Task<List<BannerDto?>> GetBannerList(CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetBannerListQuery(), cancellationToken);
    }
}