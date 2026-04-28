using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Units.Commands.UpdateUnit;

public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, Result>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMemoryCache _cache;

    public UpdateUnitCommandHandler(
        IUnitRepository unitRepository,
        IMemoryCache cache)
    {
        _unitRepository = unitRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        UpdateUnitCommand request,
        CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetByIdAsync(request.Id, cancellationToken);

        if (unit is null || unit.ProjectId != request.ProjectId)
            return Result.Failure("Unit.NotFound", "Unit not found", 404);

        unit.Name.English = request.Name.English;
        unit.Name.Arabic = request.Name.Arabic ?? unit.Name.Arabic;
        unit.Name.Italian = request.Name.Italian ?? unit.Name.Italian;
        unit.Name.Russian = request.Name.Russian ?? unit.Name.Russian;

        unit.PaymentPlan.English = request.PaymentPlan.English;
        unit.PaymentPlan.Arabic = request.PaymentPlan.Arabic ?? unit.PaymentPlan.Arabic;
        unit.PaymentPlan.Italian = request.PaymentPlan.Italian ?? unit.PaymentPlan.Italian;
        unit.PaymentPlan.Russian = request.PaymentPlan.Russian ?? unit.PaymentPlan.Russian;

        unit.Area = request.Area;
        unit.Rooms = request.Rooms;
        unit.Status = request.Status;
        unit.Type = request.Type;
        unit.Price = request.Price;
        unit.Stock = request.Stock;
        unit.ROI = request.ROI;

        await _unitRepository.UpdateAsync(unit, cancellationToken);

        _cache.Remove($"units-project-{request.ProjectId}");
        _cache.Remove($"units-{request.Id}");

        return Result.Success();
    }
}