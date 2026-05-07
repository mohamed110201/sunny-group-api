using Application.Common;
using Domain.Entities;
using Domain.Entities.Units;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Units.Commands.AddUnitDocuments;

public class AddUnitDocumentsCommandHandler : IRequestHandler<AddUnitDocumentsCommand, Result>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public AddUnitDocumentsCommandHandler(
        IUnitRepository unitRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _unitRepository = unitRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(
        AddUnitDocumentsCommand request,
        CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetByIdAsync(request.UnitId, cancellationToken);

        if (unit is null || unit.ProjectId != request.ProjectId)
            return Result.Failure("Unit.NotFound", "Unit not found", 404);

        foreach (var item in request.Documents)
        {
            var url = await _mediaStorage.SaveAsync(
                item.Document,
                $"units/{unit.Id}/documents/{item.Category.ToString().ToLower()}",
                cancellationToken);

            unit.Documents.Add(new UnitDocument
            {
                UnitId = unit.Id,
                Url = url,
                Category = item.Category
            });
        }

        await _unitRepository.UpdateAsync(unit, cancellationToken);

        _cache.Remove($"units-project-{request.ProjectId}");
        _cache.Remove($"units-{request.UnitId}");

        return Result.Success();
    }
}