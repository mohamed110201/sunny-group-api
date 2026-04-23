using Domain.ValueObjects;

namespace Domain.Entities.Projects;

public class Attraction
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public int TimeInMinutes { get; set; }
    public string IconUrl { get; set; } = string.Empty;
    public Project Project { get; set; } = null!;
}