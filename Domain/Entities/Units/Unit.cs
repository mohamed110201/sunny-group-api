using Domain.Entities.Projects;

namespace Domain.Entities.Units;

public class Unit
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}