using Application.Common.Behaviors;
using Application.Common.Options;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddApplication(this IServiceCollection services ,IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionRegister).Assembly));

        services.AddValidatorsFromAssembly(typeof(DependencyInjectionRegister).Assembly);
        services.AddMemoryCache();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        services.Configure<MediaValidationOptions>(
            options => configuration.GetSection("MediaValidation").Bind(options));
        services.Configure<DocumentValidationOptions>(
            options => configuration.GetSection("DocumentValidation").Bind(options));
        services.Configure<JwtOptions>(
            options => configuration.GetSection("Jwt").Bind(options));
        return services;
    }
}