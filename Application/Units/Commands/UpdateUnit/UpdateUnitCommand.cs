using Application.Common;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;

namespace Application.Units.Commands.UpdateUnit;

public class UpdateUnitCommand : IRequest<Result>
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public LocalizedText? Name { get; set; }
    public int? Area { get; set; }
    public LocalizedText? MetaTitle { get; set; }
    public LocalizedText? MetaDescription { get; set; }
    public int? Rooms { get; set; }
    public ProjectStatus? Status { get; set; }
    public UnitType? Type { get; set; }
    public DeliveryCondition? DeliveryCondition { get; set; }

    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public LocalizedText? PaymentPlan { get; set; }
    public double? ROI { get; set; }
}