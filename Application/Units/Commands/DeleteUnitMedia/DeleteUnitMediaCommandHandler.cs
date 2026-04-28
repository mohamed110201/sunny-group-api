using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Units.Commands.DeleteUnitMedia;

public class DeleteUnitMediaCommandHandler : IRequestHandler<DeleteUnitMediaCommand, Result>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public DeleteUnitMediaCommandHandler(
        IUnitRepository unitRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _unitRepository = unitRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(DeleteUnitMediaCommand request, CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetByIdAsync(request.UnitId, cancellationToken);

        if (unit is null || unit.ProjectId != request.ProjectId)
            return Result.Failure("Unit.NotFound", "Unit not found", 404);

        var media = unit.Media.FirstOrDefault(m => m.Id == request.MediaId);

        if (media is null)
            return Result.Failure("Media.NotFound", "Media not found", 404);

        await _mediaStorage.DeleteAsync(media.Url, cancellationToken);
        unit.Media.Remove(media);
        await _unitRepository.UpdateAsync(unit, cancellationToken);

        _cache.Remove($"units-project-{request.ProjectId}");
        _cache.Remove($"units-{request.UnitId}");

        return Result.Success();
    }
}