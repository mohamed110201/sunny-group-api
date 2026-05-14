using Application.Common;
using Application.Auth.Dtos;
using Application.Common.Interfaces;
using Application.Common.Options;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using Domain.Entities.Auth;

namespace Application.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenDto>>
{
    private readonly IAdminRepository _adminRepository;
    private readonly ITokenService _tokenService;
    private readonly JwtOptions _jwtOptions;

    public LoginCommandHandler(
        IAdminRepository adminRepository,
        ITokenService tokenService,
        IOptions<JwtOptions> jwtOptions)
    {
        _adminRepository = adminRepository;
        _tokenService = tokenService;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<Result<TokenDto>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (admin is null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.PasswordHash))
            return Result<TokenDto>.Failure("Auth.InvalidCredentials", "Invalid email or password", 401);

        var accessToken = _tokenService.GenerateAccessToken(admin);
        var refreshToken = _tokenService.GenerateRefreshToken();

        foreach (var token in admin.RefreshTokens.Where(r => r.IsActive))
            token.IsRevoked = true;

        admin.RefreshTokens.Add(new Domain.Entities.Auth.RefreshToken
        {
            Token = refreshToken,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiryDays)
        });

        await _adminRepository.UpdateAsync(admin, cancellationToken);

        return Result<TokenDto>.Success(new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken  
        });
    }
}