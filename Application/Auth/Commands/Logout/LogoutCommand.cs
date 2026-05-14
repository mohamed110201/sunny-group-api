using Application.Common;
using MediatR;

namespace Application.Auth.Commands.Logout;

public class LogoutCommand : IRequest<Result>
{
    public string Token { get; set; } = string.Empty;
}