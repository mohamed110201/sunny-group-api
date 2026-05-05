using Application.Common;
using Domain.Entities;
using Domain.Entities.Home;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Home.Commands.UpdateSettings;

public class UpdateSettingsCommandHandler : IRequestHandler<UpdateSettingsCommand, Result>
{
    private readonly ISettingsRepository _settingsRepository;
    private readonly IMemoryCache _cache;

    public UpdateSettingsCommandHandler(
        ISettingsRepository settingsRepository,
        IMemoryCache cache)
    {
        _settingsRepository = settingsRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        UpdateSettingsCommand request,
        CancellationToken cancellationToken)
    {
        var settings = await _settingsRepository.GetAsync(cancellationToken);

        if (settings is null)
        {
            settings = new Settings
            {
                CompanyFoundingYear = request.CompanyFoundingYear ?? 0,
                NumberOfClients = request.NumberOfClients ?? 0
            };
            await _settingsRepository.AddAsync(settings, cancellationToken);
        }
        else
        {
            if (request.CompanyFoundingYear.HasValue)
                settings.CompanyFoundingYear = request.CompanyFoundingYear.Value;

            if (request.NumberOfClients.HasValue)
                settings.NumberOfClients = request.NumberOfClients.Value;

            await _settingsRepository.UpdateAsync(settings, cancellationToken);
        }

        _cache.Remove("home");
        return Result.Success();
    }
}