using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Units.Dtos;

public class UnitMediaRequestDto
{
    public IFormFile Image { get; set; } = null!;
    public MediaCategory Category { get; set; }
    public MediaSubCategory? SubCategory { get; set; }

}