using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Edit;

public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;
    private readonly IFileService _fileService;
    
    public EditUserCommandHandler(
        IUserRepository userRepository,
        IUserDomainService userDomainService,
        IFileService fileService
    )
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.Id);
        if(user is null)
            return OperationResult.NotFound();

        var oldAvatar = user.AvatarName;
        user.Edit(request.Name, request.Family, request.PhoneNumber, request.Email, request.Gender, _userDomainService);
        if (request.Avatar is not null)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(request.Avatar, Directories.UserAvatar);
            user.SetAvatar(imageName);
        }
        DeleteOldAvatar(request.Avatar, oldAvatar);
        await _userRepository.Save();
        return OperationResult.Success();
    }

    private void DeleteOldAvatar(IFormFile? avatarFile, string oldImage)
    {
        if (avatarFile is null || oldImage == "avatar.png") return;
        _fileService.DeleteFile(Directories.UserAvatar, oldImage);
    }
}