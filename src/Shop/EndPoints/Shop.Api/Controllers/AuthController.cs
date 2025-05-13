using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using UAParser;

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
    public async Task<ApiResult<LoginResultDto?>> Login([FromBody] LoginViewModel viewModel)
    {
        if (ModelState.IsValid is false)
            return new ApiResult<LoginResultDto?>()
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
            return CommandResult(OperationResult<LoginResultDto?>.Error("کاربری با مشخصات وارد شده یافت نشد."));

        if (Sha256Hasher.IsCompare(user.Password, viewModel.Password) is false)
            return CommandResult(OperationResult<LoginResultDto?>.Error("کاربری با مشخصات وارد شده یافت نشد."));
        
        if (user.IsActive is false)
            return CommandResult(OperationResult<LoginResultDto?>.Error("حساب کاربری شما غیرفعال است."));

        var loginResult = await AddTokensAndGenerateJwt(user);
        return CommandResult(loginResult);
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

    [HttpPost("RefreshToken")]
    public async Task<ApiResult<LoginResultDto?>> RefreshToken([FromQuery] string refreshToken)
    {
        var result = await _userFacade.GetUserTokenByRefreshToken(refreshToken);
        if (result is null)
            return CommandResult(OperationResult<LoginResultDto?>.NotFound());

        if (result.AccessTokenExpireDate > DateTime.Now)
            return CommandResult(OperationResult<LoginResultDto?>.Error("توکن هنوز منقضی نشده است."));

        if (result.RefreshTokenExpireDate < DateTime.Now)
            return CommandResult(OperationResult<LoginResultDto?>.Error("زمان refresh token به پایان رسیده است."));


        var user = await _userFacade.GetUserById(result.UserId);
        await _userFacade.RemoveUserToken(new RemoveUserTokenCommand(result.Id, result.UserId));
        var loginResult = await AddTokensAndGenerateJwt(user);
        return CommandResult(loginResult);
    }

    [Authorize]
    [HttpDelete("Logout")]
    public async Task<ApiResult> Logout()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var result = await _userFacade.GetUserTokenByAccessToken(token);
        if (result is null)
            return CommandResult(OperationResult.NotFound());

        await _userFacade.RemoveUserToken(new RemoveUserTokenCommand(result.Id, result.UserId));
        return CommandResult(OperationResult.Success());
    }

    private async Task<OperationResult<LoginResultDto?>> AddTokensAndGenerateJwt(UserDto user)
    {
        var accessToken = JwtTokenBuilder.buildToken(user, _configuration);
        var refreshToken = Guid.NewGuid().ToString();

        var hashedAccessToken = Sha256Hasher.Hash(accessToken);
        var hashedRefreshToken = Sha256Hasher.Hash(refreshToken);

        var uaParser = Parser.GetDefault();
        var header = HttpContext.Request.Headers["user-agent"].ToString();
        var device = "windows";

        if (header is not null)
        {
            var info = uaParser.Parse(header);
            device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
        }

        var accessTokenResult = await _userFacade.AddUserToken(new AddUserTokenCommand(user.Id, hashedAccessToken, hashedRefreshToken,
            DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));

        if (accessTokenResult.Status != OperationResultStatus.Success)
            return OperationResult<LoginResultDto?>.Error();

        var result = new LoginResultDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };

        return OperationResult<LoginResultDto?>.Success(result);
    }
}