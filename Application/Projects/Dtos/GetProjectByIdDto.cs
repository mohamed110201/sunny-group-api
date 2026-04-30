using Application.Units.Dtos;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Projects.Dtos;

public class GetProjectByIdDto
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public int YearOfConstruction { get; set; }
    public int NumberOfVillas { get; set; }
    public int NumberOfApartments { get; set; }
    public int TotalUnits { get; set; }
    public double Area { get; set; }
    public LocalizedText Location { get; set; } = null!;
    public LocalizedText  Status { get; set; }
    public List<LocalizedText > Types { get; set; } = [];
    public PriceRange PriceRange { get; set; } = null!;
    public List<ProjectMediaResponseDto> Media { get; set; } = [];
    public List<AttractionResponseDto> Attractions { get; set; } = [];
    public List<GetAllUnitsDto> Units { get; set; } = [];

}