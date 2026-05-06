using Domain.ValueObjects;

namespace Application.Blogs.Dtos;

public class CreateBlogCategoryRequestDto
{
    public LocalizedText Name { get; set; } = null!;
}
