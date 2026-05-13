using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Units.Commands.UpdateUnit;

public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, Result>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMemoryCache _cache;

    public UpdateUnitCommandHandler(
        IUnitRepository unitRepository,
        IProjectRepository projectRepository,
        IMemoryCache cache)
    {
        _unitRepository = unitRepository;
        _projectRepository = projectRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        UpdateUnitCommand request,
        CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetByIdAsync(request.Id, cancellationToken);

        if (unit is null || unit.ProjectId != request.ProjectId)
            return Result.Failure("Unit.NotFound", "Unit not found", 404);

        if (request.Name != null)
        {
            unit.Name.English = request.Name.English ?? unit.Name.English;
            unit.Name.Arabic = request.Name.Arabic ?? unit.Name.Arabic;
            unit.Name.Italian = request.Name.Italian ?? unit.Name.Italian;
            unit.Name.Russian = request.Name.Russian ?? unit.Name.Russian;
        }

        if (request.PaymentPlan != null)
        {
            unit.PaymentPlan.English = request.PaymentPlan.English ?? unit.PaymentPlan.English;
            unit.PaymentPlan.Arabic = request.PaymentPlan.Arabic ?? unit.PaymentPlan.Arabic;
            unit.PaymentPlan.Italian = request.PaymentPlan.Italian ?? unit.PaymentPlan.Italian;
            unit.PaymentPlan.Russian = request.PaymentPlan.Russian ?? unit.PaymentPlan.Russian;
        }

        if (request.Area.HasValue)
            unit.Area = request.Area.Value;

        if (request.MetaTitle != null)
            unit.MetaTitle = request.MetaTitle;

        if (request.MetaDescription != null)
            unit.MetaDescription = request.MetaDescription;

        if (request.Rooms.HasValue)
            unit.Rooms = request.Rooms.Value;

        if (request.Status.HasValue)
            unit.Status = request.Status.Value;

        if (request.Type.HasValue)
            unit.Type = request.Type.Value;

        if (request.DeliveryCondition.HasValue)
            unit.DeliveryCondition = request.DeliveryCondition.Value;

        if (request.Price.HasValue)
            unit.Price = request.Price.Value;

        if (request.Stock.HasValue)
            unit.Stock = request.Stock.Value;

        if (request.ROI.HasValue)
            unit.ROI = request.ROI.Value;

        await _unitRepository.UpdateAsync(unit, cancellationToken);

        await _projectRepository.UpdatePriceRangeAsync(request.ProjectId, cancellationToken);

        _cache.Remove($"units-project-{request.ProjectId}");
        _cache.Remove($"units-{request.Id}");
        _cache.Remove("units-all");
        _cache.Remove("projects-all");
        _cache.Remove($"projects-{request.ProjectId}");
        return Result.Success();
    }
}