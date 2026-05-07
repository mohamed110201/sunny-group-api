using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Projects.Dtos;

public class ProjectDocumentRequestDto
{
    public IFormFile Document { get; set; } = null!;
    public DocumentCategory Category { get; set; }
}