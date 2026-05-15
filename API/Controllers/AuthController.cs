using API.Common;
using API.Common.Helpers;
using API.Common.JSend;
using Application.Auth.Commands.Login;
using Application.Auth.Commands.Logout;
using Application.Auth.Commands.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : BaseController
{
    private readonly IMediator _mediator;
    private readonly CookieHelper _cookieHelper;

    public AuthController(IMediator mediator, CookieHelper cookieHelper)
    {
        _mediator = mediator;
        _cookieHelper = cookieHelper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
            _cookieHelper.SetAuthCookies(Response, result.Value!.RefreshToken);

        return HandleResult(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
            return JSendHelper.Unauthorized(message: "Refresh token not found");

        var result = await _mediator.Send(
            new RefreshTokenCommand { Token = refreshToken },
            cancellationToken);

        if (result.IsSuccess)
            _cookieHelper.SetAuthCookies(Response, result.Value!.RefreshToken);

        return HandleResult(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
            return JSendHelper.Unauthorized(message: "Refresh token not found");

        var result = await _mediator.Send(
            new LogoutCommand { Token = refreshToken },
            cancellationToken);

        _cookieHelper.ClearAuthCookies(Response);

        return HandleResult(result);
    }
}