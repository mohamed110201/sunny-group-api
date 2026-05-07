using Application.Common;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;

namespace Application.Units.Commands.CreateUnit;

public class CreateUnitCommand : IRequest<Result<int>>
{
    public int ProjectId { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public int Area { get; set; }
    public int Rooms { get; set; }
    public ProjectStatus Status { get; set; }
    public UnitType Type { get; set; }
    public DeliveryCondition DeliveryCondition { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public LocalizedText PaymentPlan { get; set; } = null!;
    public double ROI { get; set; }
}