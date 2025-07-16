using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.Banners.Delete;

public record DeleteBannerCommand(long BannerId): IBaseCommand;


public class DeleteBannerCommandHandler : IBaseCommandHandler<DeleteBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IFileService _fileService;

    public DeleteBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var slider = await _bannerRepository.GetTracking(request.BannerId);
        if (slider == null) return OperationResult.NotFound();

        _bannerRepository.DeleteBanner(slider);
        _fileService.DeleteFile(Directories.BannerImages, slider.ImageName);
        await _bannerRepository.Save();
        return OperationResult.Success();
    }
}