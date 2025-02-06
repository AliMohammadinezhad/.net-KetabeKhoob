using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Presentation.Facade.SiteEntities.Banners;
using Shop.Query.SiteEntities.Banners.DTOs;

namespace Shop.Api.Controllers;

public class BannerController : ApiController
{
    private readonly IBannerFacade _bannerFacade;

    public BannerController(IBannerFacade bannerFacade)
    {
        _bannerFacade = bannerFacade;
    }

    [HttpGet("{bannerId:long}")]
    public async Task<ApiResult<BannerDto?>> GetBannerById(long bannerId)
    {
        var result = await _bannerFacade.GetBannerById(bannerId);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<List<BannerDto?>?>> GetBannerList()
    {
        var result = await _bannerFacade.GetBannerList();
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> CreateBanner([FromBody] CreateBannerCommand command)
    {
        var result = await _bannerFacade.CreateBanner(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditBanner([FromBody] EditBannerCommand command)
    {
        var result = await _bannerFacade.EditBanner(command);
        return CommandResult(result);
    }
}