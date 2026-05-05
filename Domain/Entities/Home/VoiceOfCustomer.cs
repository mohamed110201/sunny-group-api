using Domain.Entities.Projects;
using Domain.ValueObjects;

namespace Domain.Entities.Home;

public class VoiceOfCustomer
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public LocalizedText Comment { get; set; } = null!;
    public Project Project { get; set; } = null!;
}