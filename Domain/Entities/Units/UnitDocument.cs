using Domain.Enums;

namespace Domain.Entities.Units;

public class UnitDocument
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string Url { get; set; } = string.Empty;
    public DocumentCategory Category { get; set; }
    public Unit Unit { get; set; } = null!;
}