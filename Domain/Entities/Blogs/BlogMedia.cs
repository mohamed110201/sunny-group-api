using Domain.Enums;

namespace Domain.Entities.Blogs;

public class BlogMedia
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Url { get; set; } = string.Empty;
    public MediaCategory Category { get; set; }
    public Blog Blog { get; set; } = null!;
}