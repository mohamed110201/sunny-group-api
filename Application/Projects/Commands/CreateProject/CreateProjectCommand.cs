using Application.Common;
using Application.Projects.Dtos;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Result<int>>
{
    public LocalizedText Name { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public int YearOfConstruction { get; set; }
    public int NumberOfVillas { get; set; }
    public int NumberOfApartments { get; set; }
    public double Area { get; set; }
    public LocalizedText  Location { get; set; } = null!;
    public ProjectStatus Status { get; set; }
    public List<ProjectType> Types { get; set; } = [];
    public PriceRange PriceRange { get; set; } = null!;
}
