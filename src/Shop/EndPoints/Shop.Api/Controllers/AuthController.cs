using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;

namespace Shop.Api.Controllers;

public class AuthController : ApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IConfiguration _configuration;

    public AuthController(IUserFacade userFacade, IConfiguration configuration)
    {
        _userFacade = userFacade;
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public async Task<ApiResult<string>> Login([FromBody] LoginViewModel viewModel)
    {
        if (ModelState.IsValid is false)
            return new ApiResult<string>()
            {
                Data = null,
                IsSuccess = false,
                MetaData = new()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = JoinErrors()
                }
            };

        var user = await _userFacade.GetUserByPhoneNumber(viewModel.PhoneNumber);
        if (user is null)
            return CommandResult(OperationResult<string>.Error("کاربری با مشخصات وارد شده یافت نشد."));

        if (Sha256Hasher.IsCompare(user.Password, viewModel.Password) is false)
            return CommandResult(OperationResult<string>.Error("کاربری با مشخصات وارد شده یافت نشد."));
        
        if (user.IsActive is false)
            return CommandResult(OperationResult<string>.Error("حساب کاربری شما غیرفعال است."));

        var token = JwtTokenBuilder.buildToken(user, _configuration);
        return new ApiResult<string>()
        {
            Data = token,
            IsSuccess = true,
            MetaData = new()
        };
    }

    [HttpPost("Register")]
    public async Task<ApiResult> Register([FromBody] RegisterViewModel viewModel)
    {
        if (ModelState.IsValid is false)
            return new ApiResult()
            {
                IsSuccess = false,
                MetaData = new MetaData
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = JoinErrors()
                }
            };

        var command = new RegisterUserCommand(new PhoneNumber(viewModel.PhoneNumber), viewModel.Password);
        var result = await _userFacade.RegisterUser(command);
        return CommandResult(result);
    }
}