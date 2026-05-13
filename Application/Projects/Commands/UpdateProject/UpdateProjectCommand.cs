using Application.Common;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;

namespace Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<Result>
{
    public int Id { get; set; }
    public LocalizedText? Name { get; set; }
    public LocalizedText? Description { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public int? YearOfConstruction { get; set; }
    public int? NumberOfVillas { get; set; }
    public int? NumberOfApartments { get; set; }
    public double? Area { get; set; }
    public LocalizedText? Location { get; set; }
    public ProjectStatus? Status { get; set; }
    public List<ProjectType>? Types { get; set; }
}