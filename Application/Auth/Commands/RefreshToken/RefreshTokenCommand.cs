using Application.Auth.Dtos;
using Application.Common;
using MediatR;

namespace Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<Result<TokenDto>>
{
    public string Token { get; set; } = string.Empty;
}