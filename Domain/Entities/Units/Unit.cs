using Domain.Entities.Projects;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities.Units;

public class Unit
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public int Area { get; set; }
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public int Rooms { get; set; }
    public ProjectStatus Status { get; set; }
    public UnitType Type { get; set; }
    public DeliveryCondition  DeliveryCondition { get; set; } 
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public LocalizedText PaymentPlan { get; set; } = null!;
    public double ROI { get; set; }
    public Project Project { get; set; } = null!;
    public ICollection<UnitMedia> Media { get; set; } = [];
    public ICollection<UnitDocument> Documents { get; set; } = [];

}