using Common.Query;
using Shop.Query.SiteEntities.Sliders.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetList;

public record GetSliderListQuery : IQuery<List<SliderDto?>>;