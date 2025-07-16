using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Delete;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.GetById;
using Shop.Query.SiteEntities.Sliders.DTOs;
using Shop.Query.SiteEntities.Sliders.GetById;
using Shop.Query.SiteEntities.Sliders.GetList;

namespace Shop.Presentation.Facade.SiteEntities.Sliders;

internal class SliderFacade : ISliderFacade
{
    private readonly IMediator _mediator;

    public SliderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateSlider(CreateSliderCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> EditSlider(EditSliderCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> DeleteSlider(long sliderId, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new DeleteSliderCommand(sliderId), cancellationToken);
    }

    public async Task<SliderDto?> GetSliderById(long id, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetSliderByIdQuery(id), cancellationToken);
    }

    public async Task<List<SliderDto?>> GetSliderList(CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetSliderListQuery(), cancellationToken);
    }
}