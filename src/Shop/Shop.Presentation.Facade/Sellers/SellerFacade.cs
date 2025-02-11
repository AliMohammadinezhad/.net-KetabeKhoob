using Common.Application;
using MediatR;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.GetByFilter;
using Shop.Query.Sellers.GetById;
using Shop.Query.Sellers.GetByUserId;

namespace Shop.Presentation.Facade.Sellers;

internal class SellerFacade : ISellerFacade
{
    private readonly IMediator _mediator;

    public SellerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateSeller(CreateSellerCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<OperationResult> EditSeller(EditSellerCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<SellerDto?> GetSellerById(long id, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetSellerByIdQuery(id), cancellationToken);
    }

    public async Task<SellerDto?> GetSellerByUserId(long userId, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetSellerByUserIdQuery(userId), cancellationToken);
    }

    public async Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetSellerByFilterQuery(filterParams), cancellationToken);
    }
}