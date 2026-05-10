using Domain.ValueObjects;

namespace Application.Blogs.Dtos;

public class GetBlogByIdDto
{
    public int Id { get; set; }
    public LocalizedText Title { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public LocalizedText Content { get; set; } = null!;
    public LocalizedText Category { get; set; } = null!;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public BlogMediaResponseDto? Cover { get; set; }
    public List<BlogMediaResponseDto> Gallery { get; set; } = [];
}
