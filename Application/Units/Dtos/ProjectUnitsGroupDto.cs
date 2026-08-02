using Domain.ValueObjects;

namespace Application.Units.Dtos;

public class ProjectUnitsGroupDto
{
    public int ProjectId { get; set; }
    public LocalizedText ProjectName { get; set; } = null!;
    public LocalizedText ProjectLocation { get; set; } = null!;
    public string? MapUrl { get; set; }
    public List<GetAllUnitsDto> Units { get; set; } = [];
}