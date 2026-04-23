using Domain.Enums;

namespace Domain.Entities.Projects;

public class ProjectMedia
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Url { get; set; } = string.Empty;
    public MediaCategory Category { get; set; }
    public Project Project { get; set; } = null!;
}