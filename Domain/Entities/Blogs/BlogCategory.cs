using Domain.ValueObjects;

namespace Domain.Entities.Blogs;

public class BlogCategory
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public ICollection<Blog> Blogs { get; set; } = [];
}
