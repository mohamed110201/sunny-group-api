using Domain.ValueObjects;

namespace Application.Projects.Dtos;

public class AttractionResponseDto
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public int TimeInMinutes { get; set; }
    public string IconUrl { get; set; } = string.Empty;
}