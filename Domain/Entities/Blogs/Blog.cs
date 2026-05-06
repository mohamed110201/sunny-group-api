using Domain.ValueObjects;

namespace Domain.Entities.Blogs;

public class Blog
{
    public int Id { get; set; }
    public LocalizedText Title { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public LocalizedText Content { get; set; } = null!;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public BlogCategory Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public ICollection<BlogMedia> Media { get; set; } = [];
}