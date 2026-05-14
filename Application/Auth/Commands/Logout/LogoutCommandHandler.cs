using Application.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Auth.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IAdminRepository _adminRepository;

    public LogoutCommandHandler(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public async Task<Result> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.GetByRefreshTokenAsync(request.Token, cancellationToken);

        if (admin is null)
            return Result.Failure("Auth.InvalidToken", "Invalid refresh token", 401);

        var refreshToken = admin.RefreshTokens.SingleOrDefault(r => r.Token == request.Token);

        if (refreshToken is null || !refreshToken.IsActive)
            return Result.Failure("Auth.TokenExpired", "Refresh token is already expired or revoked", 401);

        refreshToken.IsRevoked = true;

        await _adminRepository.UpdateAsync(admin, cancellationToken);

        return Result.Success();
    }
}