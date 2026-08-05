using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Units.Dtos;

public class GetAllUnitsDto
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public int Area { get; set; }
    public LocalizedText MetaTitle { get; set; } = null!;
    public LocalizedText MetaDescription { get; set; } = null!;
    public int Rooms { get; set; }
    
    public int NumberOfBathrooms { get; set; }
    public LocalizedText Status { get; set; }
    public LocalizedText Type { get; set; }
    public LocalizedText DeliveryCondition { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public double ROI { get; set; }
    public decimal PaidToStartEarnPercentage { get; set; }
    public decimal AnnualYield { get; set; }
    public UnitMediaResponseDto? CoverImage { get; set; }
    public string? ProjectIconUrl { get; set; }
    public string? ProjectMapUrl { get; set; }


}