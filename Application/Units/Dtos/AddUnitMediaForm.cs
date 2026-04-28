using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Units.Dtos;

public class AddUnitMediaForm
{
    public List<IFormFile> Images { get; set; } = [];
    public List<MediaCategory> Categories { get; set; } = [];
    public List<MediaSubCategory?> SubCategories { get; set; } = [];
}