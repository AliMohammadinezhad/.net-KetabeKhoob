using Common.Application;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Query.Sellers.DTOs;
using Shop.Query.SiteEntities.Sliders.DTOs;

namespace Shop.Presentation.Facade.SiteEntities.Sliders;

public interface ISliderFacade
{
    Task<OperationResult> CreateSlider(CreateSliderCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> EditSlider(EditSliderCommand command, CancellationToken cancellationToken = default);

    Task<SliderDto?> GetSliderById(long id, CancellationToken cancellationToken = default);
    Task<List<SliderDto?>> GetSliderList(CancellationToken cancellationToken = default);
}