using Domain.Enums;

namespace Application.Units.Dtos;

public class UnitDocumentResponseDto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public DocumentCategory Category { get; set; }
}