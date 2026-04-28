using Domain.Enums;

namespace Application.Units.Dtos;

public class UnitMediaResponseDto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public MediaCategory Category { get; set; }
    public MediaSubCategory? SubCategory { get; set; }
}