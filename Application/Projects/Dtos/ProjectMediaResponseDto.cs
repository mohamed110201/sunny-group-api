using Domain.Enums;

namespace Application.Projects.Dtos;

public class ProjectMediaDto
{
    public string Url { get; set; } = string.Empty;
    public MediaCategory Category { get; set; }

}