using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Projects.Dtos;

public class ProjectMediaRequestDto
{
    public IFormFile Image { get; set; } = null!;
    public MediaCategory Category { get; set; }
}