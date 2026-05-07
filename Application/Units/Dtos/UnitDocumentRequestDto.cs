using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Units.Dtos;

public class UnitDocumentRequestDto
{
    public IFormFile Document { get; set; } = null!;
    public DocumentCategory Category { get; set; }
}