using Domain.ValueObjects;

namespace Application.Blogs.Dtos;

public class GetBlogByIdDto
{
    public int Id { get; set; }
    public LocalizedText Title { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public int ReadTimeMinutes { get; set; }

    public LocalizedText Content { get; set; } = null!;
    public LocalizedText Category { get; set; } = null!;
    public LocalizedText MetaTitle { get; set; } = null!;
    public LocalizedText MetaDescription { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public BlogMediaResponseDto? Cover { get; set; }
    public List<BlogMediaResponseDto> Gallery { get; set; } = [];
}
