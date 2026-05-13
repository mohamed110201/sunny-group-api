using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Projects.Dtos;

public class GetAllProjectsDto
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public int YearOfConstruction { get; set; }
    public int NumberOfVillas { get; set; }
    public int NumberOfApartments { get; set; }
    public int TotalUnits { get; set; }
    public double Area { get; set; }
    public LocalizedText Location { get; set; } = null!;
    public LocalizedText Status { get; set; }
    public List<LocalizedText> Types { get; set; } = [];
    public PriceRange? PriceRange { get; set; }
    public ProjectMediaResponseDto CoverImage { get; set; } = null!;

}