using Domain.ValueObjects;

namespace Application.Blogs.Dtos;

public class GetAllBlogsDto
{
    public int Id { get; set; }
    public LocalizedText Title { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public LocalizedText MetaTitle { get; set; } = null!;
    public LocalizedText MetaDescription { get; set; } = null!;
    public LocalizedText Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public BlogMediaResponseDto? Cover { get; set; }
}
