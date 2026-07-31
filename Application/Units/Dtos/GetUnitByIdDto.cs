using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Units.Dtos;

public class GetUnitByIdDto
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
    public LocalizedText PaymentPlan { get; set; } = null!;
    public double ROI { get; set; }
    public decimal PaidToStartEarn { get; set; }
    public decimal MonthlyRent { get; set; }
    public int YearsToEarn { get; set; }
    public decimal PaidToStartEarnPercentage { get; set; }
    public decimal AnnualYield { get; set; }
    public UnitMediaResponseDto? CoverImage { get; set; }
    public List<UnitMediaResponseDto> GalleryImages { get; set; } = [];
    public List<UnitMediaResponseDto> InventoryImages { get; set; } = [];
    public List<UnitDocumentResponseDto> Documents { get; set; } = [];
    public string? ProjectIconUrl { get; set; }

}