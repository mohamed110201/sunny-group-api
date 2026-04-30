using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Application.Projects.Dtos;

public record AttractionRequestDto(
    LocalizedText Name,
    int TimeInMinutes,
    IFormFile Icon
);

