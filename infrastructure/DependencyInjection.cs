using Application.Common.Interfaces;
using Domain.Interfaces;
using infrastructure.Persistence;
using infrastructure.Repositories;
using Infrastructure.Repositories;
using infrastructure.Services;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddScoped<ISettingsRepository, SettingsRepository>();
        services.AddScoped<IHtmlSanitizerService, HtmlSanitizerService>();

        return services;
    }
}