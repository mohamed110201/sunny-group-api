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
    public LocalizedText MetaTitle { get; set; } = null!;
    public LocalizedText MetaDescription { get; set; } = null!;
    public int Rooms { get; set; }
    public int NumberOfBathrooms { get; set; }

    public UnitStatus Status { get; set; }
    public UnitType Type { get; set; }
    public DeliveryCondition DeliveryCondition { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public LocalizedText PaymentPlan { get; set; } = null!;
    public double ROI { get; set; }
}