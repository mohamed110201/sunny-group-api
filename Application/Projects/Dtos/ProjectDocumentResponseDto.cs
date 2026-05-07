using Domain.Enums;

namespace Application.Projects.Dtos;

public class ProjectDocumentResponseDto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public DocumentCategory Category { get; set; }
}