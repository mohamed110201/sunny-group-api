using Application.Auth.Dtos;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Options;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenDto>>
{
    private readonly IAdminRepository _adminRepository;
    private readonly ITokenService _tokenService;
    private readonly JwtOptions _jwtOptions;

    public RefreshTokenCommandHandler(
        IAdminRepository adminRepository,
        ITokenService tokenService,
        IOptions<JwtOptions> jwtOptions)
    {
        _adminRepository = adminRepository;
        _tokenService = tokenService;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<Result<TokenDto>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.GetByRefreshTokenAsync(request.Token, cancellationToken);

        if (admin is null)
            return Result<TokenDto>.Failure("Auth.InvalidToken", "Invalid refresh token", 401);

        var refreshToken = admin.RefreshTokens.Single(r => r.Token == request.Token);

        if (!refreshToken.IsActive)
            return Result<TokenDto>.Failure("Auth.TokenExpired", "Refresh token is expired or revoked", 401);

        refreshToken.IsRevoked = true;

        var newAccessToken = _tokenService.GenerateAccessToken(admin);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        admin.RefreshTokens.Add(new Domain.Entities.Auth.RefreshToken
        {
            Token = newRefreshToken,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiryDays)
        });

        await _adminRepository.UpdateAsync(admin, cancellationToken);

        return Result<TokenDto>.Success(new TokenDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }
}