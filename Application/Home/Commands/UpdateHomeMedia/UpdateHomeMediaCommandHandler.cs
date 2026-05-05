using Application.Common;
using Domain.Entities;
using Domain.Entities.Home;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Home.Commands.UpdateHomeMedia;

public class UpdateHomeMediaCommandHandler : IRequestHandler<UpdateHomeMediaCommand, Result>
{
    private readonly ISettingsRepository _settingsRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public UpdateHomeMediaCommandHandler(
        ISettingsRepository settingsRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _settingsRepository = settingsRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(
        UpdateHomeMediaCommand request,
        CancellationToken cancellationToken)
    {
        var settings = await _settingsRepository.GetAsync(cancellationToken);

        if (settings?.HomeMediaUrl is not null)
            await _mediaStorage.DeleteAsync(settings.HomeMediaUrl, cancellationToken);

        var url = await _mediaStorage.SaveAsync(request.Media, "home", cancellationToken);

        if (settings is null)
        {
            settings = new Settings { HomeMediaUrl = url };
            await _settingsRepository.AddAsync(settings, cancellationToken);
        }
        else
        {
            settings.HomeMediaUrl = url;
            await _settingsRepository.UpdateAsync(settings, cancellationToken);
        }

        _cache.Remove("home");
        return Result.Success();
    }
}