using Domain.Enums;

namespace Application.Blogs.Dtos;

public class BlogMediaResponseDto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public MediaCategory Category { get; set; }
}