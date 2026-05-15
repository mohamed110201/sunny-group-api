using Application.Common.Options;
using Microsoft.Extensions.Options;

namespace API.Common.Helpers;

public class CookieHelper
{
    private readonly JwtOptions _jwtOptions;

    public CookieHelper(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public void SetAuthCookies(HttpResponse response, string refreshToken)
    {
        response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiryDays).ToLocalTime()
        });
    }

    public void ClearAuthCookies(HttpResponse response)
    {
        response.Cookies.Delete("accessToken");
        response.Cookies.Delete("refreshToken");
    }
}