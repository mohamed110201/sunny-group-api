using Application.Common;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Units.Commands.DeleteUnitDocument;

public class DeleteUnitDocumentCommandHandler : IRequestHandler<DeleteUnitDocumentCommand, Result>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public DeleteUnitDocumentCommandHandler(
        IUnitRepository unitRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _unitRepository = unitRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(
        DeleteUnitDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetByIdAsync(request.UnitId, cancellationToken);

        if (unit is null || unit.ProjectId != request.ProjectId)
            return Result.Failure("Unit.NotFound", "Unit not found", 404);

        var document = unit.Documents.FirstOrDefault(d => d.Id == request.DocumentId);

        if (document is null)
            return Result.Failure("Document.NotFound", "Document not found", 404);

        await _mediaStorage.DeleteAsync(document.Url, cancellationToken);
        unit.Documents.Remove(document);
        await _unitRepository.UpdateAsync(unit, cancellationToken);

        _cache.Remove($"units-project-{request.ProjectId}");
        _cache.Remove($"units-{request.UnitId}");

        return Result.Success();
    }
}