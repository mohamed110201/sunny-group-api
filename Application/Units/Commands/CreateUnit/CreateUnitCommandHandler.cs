using Application.Common;
using UnitEntity = Domain.Entities.Units.Unit;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Units.Commands.CreateUnit;

public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, Result<int>>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMemoryCache _cache;

    public CreateUnitCommandHandler(
        IUnitRepository unitRepository,
        IProjectRepository projectRepository,
        IMemoryCache cache)
    {
        _unitRepository = unitRepository;
        _projectRepository = projectRepository;
        _cache = cache;
    }

    public async Task<Result<int>> Handle(
        CreateUnitCommand request,
        CancellationToken cancellationToken)
    {
        var projectExists = await _projectRepository.ExistsAsync(request.ProjectId, cancellationToken);

        if (!projectExists)
            return Result<int>.Failure("Project.NotFound", "Project not found", 404);

        var unit = new UnitEntity
        {
            ProjectId = request.ProjectId,
            Name = request.Name,
            Area = request.Area,
            MetaTitle = request.MetaTitle,
            MetaDescription = request.MetaDescription,
            Rooms = request.Rooms,
            Status = request.Status,
            Type = request.Type,
            DeliveryCondition = request.DeliveryCondition,
            Price = request.Price,
            Stock = request.Stock,
            PaymentPlan = request.PaymentPlan,
            ROI = request.ROI
        };

        await _unitRepository.AddAsync(unit, cancellationToken);

        // Update project's price range based on all units
        await _projectRepository.UpdatePriceRangeAsync(request.ProjectId, cancellationToken);

        _cache.Remove($"units-project-{request.ProjectId}");
        _cache.Remove("units-all");
        return Result<int>.Success(unit.Id);
    }
}