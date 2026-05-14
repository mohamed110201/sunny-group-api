using Application.Common;
using Application.Auth.Dtos;
using MediatR;

namespace Application.Auth.Commands.Login;

public class LoginCommand : IRequest<Result<TokenDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}