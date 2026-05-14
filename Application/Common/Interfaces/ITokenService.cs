using Domain.Entities;
using Domain.Entities.Auth;

namespace Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Admin admin);
    string GenerateRefreshToken();
}