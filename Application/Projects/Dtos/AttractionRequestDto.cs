using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Application.Projects.Dtos;

public record AttractionRequest(
    LocalizedText Name,
    int TimeInMinutes,
    IFormFile Icon
);