using Domain.ValueObjects;

namespace Application.Blogs.Dtos;

public class BlogCategoryResponseDto
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public bool IsActive { get; set; }
}
