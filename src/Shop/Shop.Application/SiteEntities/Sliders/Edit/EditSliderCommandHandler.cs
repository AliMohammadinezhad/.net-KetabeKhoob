using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.Sliders.Edit;

public class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
{
    private readonly ISliderRepository _sliderRepository;
    private readonly IFileService _fileService;

    public EditSliderCommandHandler(ISliderRepository sliderRepository, IFileService fileService)
    {
        _sliderRepository = sliderRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetTracking(request.Id);
        if (slider is null)
            return OperationResult.NotFound();

        var imageName = slider.ImageName;
        var oldImage = slider.ImageName;
        if (request.ImageFile is not null)
            imageName = await _fileService.SaveFileAndGenerateName(
                request.ImageFile, Directories.SliderImages
            );

        slider.Edit(request.Title, request.Link, imageName, request.Position);
        await _sliderRepository.Save();
        DeleteOldImage(request.ImageFile, oldImage);
        return OperationResult.Success();
    }

    private void DeleteOldImage(IFormFile? imageFile, string oldImage)
    {
        if(imageFile is not null)
            _fileService.DeleteFile(Directories.SliderImages, oldImage);
    }
}