using Common.Application;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Query.Sellers.DTOs;

namespace Shop.Presentation.Facade.Sellers;

public interface ISellerFacade
{

    Task<OperationResult> CreateSeller(CreateSellerCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> EditSeller(EditSellerCommand command, CancellationToken cancellationToken = default);

    Task<SellerDto?> GetSellerById(long id, CancellationToken cancellationToken = default);
    Task<SellerDto?> GetSellerByUserId(long userId, CancellationToken cancellationToken = default);
    Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams , CancellationToken cancellationToken = default);
}