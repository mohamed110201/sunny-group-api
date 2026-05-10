using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Units;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Units.Commands.AddUnitMedia;

public class AddUnitMediaCommandHandler : IRequestHandler<AddUnitMediaCommand, Result>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public AddUnitMediaCommandHandler(
        IUnitRepository unitRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _unitRepository = unitRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(AddUnitMediaCommand request, CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetByIdAsync(request.UnitId, cancellationToken);

        if (unit is null || unit.ProjectId != request.ProjectId)
            return Result.Failure("Unit.NotFound", "Unit not found", 404);

        var incomingCovers = request.Images.Count(i => i.Category == MediaCategory.Cover);

        if (incomingCovers > 1)
            return Result.Failure("Media.InvalidCover", "Only one cover image is allowed", 400);

        if (incomingCovers == 1 && unit.Media.Any(m => m.Category == MediaCategory.Cover))
            return Result.Failure("Media.CoverExists", "Unit already has a cover image. Delete it first", 400);

        foreach (var item in request.Images)
        {
            var folder = item.SubCategory != null
                ? $"units/{unit.Id}/{item.Category.ToString().ToLower()}/{item.SubCategory?.ToString().ToLower()}"
                : $"units/{unit.Id}/{item.Category.ToString().ToLower()}";

            var url = await _mediaStorage.SaveAsync(item.Image, folder, cancellationToken);

            unit.Media.Add(new UnitMedia
            {
                UnitId = unit.Id,
                Url = url,
                Category = item.Category,
                SubCategory = item.SubCategory
            });
        }

        await _unitRepository.UpdateAsync(unit, cancellationToken);

        _cache.Remove($"units-project-{request.ProjectId}");
        _cache.Remove($"units-{request.UnitId}");

        return Result.Success();
    }
}