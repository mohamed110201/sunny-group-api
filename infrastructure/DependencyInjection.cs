using System.Text;
using Application.Common.Interfaces;
using Application.Common.Options;
using Domain.Interfaces;
using infrastructure.Persistence;
using infrastructure.Repositories;
using Infrastructure.Repositories;
using infrastructure.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddMemoryCache();

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<IMediaStorageService, MediaStorageService>();
        services.AddScoped<IInquiryRepository, InquiryRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
        services.AddScoped<IVoiceOfCustomerRepository, VoiceOfCustomerRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        services.AddScoped<ISettingsRepository, SettingsRepository>();
        services.AddScoped<IHtmlSanitizerService, HtmlSanitizerService>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<ITokenService, TokenService>();

        var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>()!;

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

            });

        services.AddAuthorization();
        return services;
    }
}