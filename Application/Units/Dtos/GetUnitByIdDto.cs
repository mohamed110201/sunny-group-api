using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Units.Dtos;

public class GetUnitByIdDto
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public int Area { get; set; }
    public int Rooms { get; set; }
    public LocalizedText  Status { get; set; }
    public LocalizedText  Type { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public LocalizedText PaymentPlan { get; set; } = null!;
    public double ROI { get; set; }
    public UnitMediaResponseDto? CoverImage { get; set; }
    public List<UnitMediaResponseDto> GalleryImages { get; set; } = [];
    public List<UnitMediaResponseDto> InventoryImages { get; set; } = [];
}