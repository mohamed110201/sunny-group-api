using Domain.Entities.Units;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities.Projects;

public class Project
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public int YearOfConstruction { get; set; }
    public int NumberOfVillas { get; set; }
    public int NumberOfApartments { get; set; }
    public int TotalUnits => NumberOfVillas + NumberOfApartments;
    public double Area { get; set; }
    public LocalizedText  Location { get; set; }= null!;
    public ProjectStatus Status { get; set; }
    public List<ProjectType> Types { get; set; } = [];
    public PriceRange PriceRange { get; set; } = null!;
    public ICollection<ProjectMedia> Media { get; set; } = [];
    public ICollection<ProjectDocument> Documents { get; set; } = [];

    public ICollection<Attraction> Attractions { get; set; } = [];
    public ICollection<Unit> Units { get; set; } = [];
}