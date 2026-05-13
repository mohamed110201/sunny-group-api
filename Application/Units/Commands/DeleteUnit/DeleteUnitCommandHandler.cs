using Application.Common;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Units.Commands.DeleteUnit;

public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, Result>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public DeleteUnitCommandHandler(
        IUnitRepository unitRepository,
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _unitRepository = unitRepository;
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(
        DeleteUnitCommand request,
        CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetByIdAsync(request.UnitId, cancellationToken);

        if (unit is null || unit.ProjectId != request.ProjectId)
            return Result.Failure("Unit.NotFound", "Unit not found", 404);

        foreach (var media in unit.Media)
            await _mediaStorage.DeleteAsync(media.Url, cancellationToken);

        await _unitRepository.DeleteAsync(unit, cancellationToken);

        // Update project's price range based on remaining units
        await _projectRepository.UpdatePriceRangeAsync(request.ProjectId, cancellationToken);

        _cache.Remove($"units-project-{request.ProjectId}");
        _cache.Remove($"units-{request.UnitId}");
        _cache.Remove("units-all");
        return Result.Success();
    }
}