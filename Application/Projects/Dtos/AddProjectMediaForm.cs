using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Projects.Dtos;

public class AddProjectMediaForm
{
    public List<IFormFile> Images { get; set; } = [];
    public List<MediaCategory> Categories { get; set; } = [];
}