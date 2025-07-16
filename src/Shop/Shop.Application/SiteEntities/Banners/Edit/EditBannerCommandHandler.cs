using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommandHandler : IBaseCommandHandler<EditBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IFileService _fileService;

    public EditBannerCommandHandler(
        IBannerRepository bannerRepository,
        IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _bannerRepository.GetTracking(request.Id);
        if (banner is null)
            return OperationResult.NotFound();

        var imageName = banner.ImageName;
        var oldImage = banner.ImageName;

        if (request.ImageFile is not null)
            imageName = await _fileService.SaveFileAndGenerateName(
                request.ImageFile, Directories.BannerImages
            );

        banner.Edit(request.Link, imageName, request.Position);
        DeleteOldImage(request.ImageFile, oldImage);
        await _bannerRepository.Save();
        return OperationResult.Success();
    }

    private void DeleteOldImage(IFormFile? imageFile, string oldImage)
    {
        if (imageFile is not null)
            _fileService.DeleteFile(Directories.BannerImages, oldImage);
    }
}