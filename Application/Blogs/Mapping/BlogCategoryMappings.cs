using Application.Blogs.Dtos;
using Domain.Entities.Blogs;

namespace Application.Blogs.Mapping;

public  static class BlogCategoryMappings
{
    public static BlogCategoryResponseDto ToDto(this BlogCategory category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        IsActive = category.IsActive
    };
}