using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.SiteEntities.Sliders;
using Shop.Query.SiteEntities.Sliders.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.CrudSlider)]
public class SliderController : ApiController
{
    private readonly ISliderFacade _sliderFacade;

    public SliderController(ISliderFacade sliderFacade)
    {
        _sliderFacade = sliderFacade;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<List<SliderDto?>?>> GetSliderList()
    {
        var result = await _sliderFacade.GetSliderList();
        return QueryResult(result);
    }

    [HttpGet("{sliderId}")]
    public async Task<ApiResult<SliderDto?>> GetSliderById(long sliderId)
    {
        var result = await _sliderFacade.GetSliderById(sliderId);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> CreateSlider([FromBody] CreateSliderCommand command)
    {
        var result = await _sliderFacade.CreateSlider(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditSlider([FromBody] EditSliderCommand command)
    {
        var result = await _sliderFacade.EditSlider(command);
        return CommandResult(result);
    }
}