using Domain.Enums;

namespace Domain.Entities.Units;

public class UnitMedia
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string Url { get; set; } = string.Empty;
    public MediaCategory Category { get; set; }
    public MediaSubCategory? SubCategory { get; set; }
    public Unit Unit { get; set; } = null!;
}