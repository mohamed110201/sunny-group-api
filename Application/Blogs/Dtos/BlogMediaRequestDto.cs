using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Blogs.Dtos;

public class BlogMediaRequestDto
{
    public IFormFile File { get; set; } = null!;
    public MediaCategory Category { get; set; }
}