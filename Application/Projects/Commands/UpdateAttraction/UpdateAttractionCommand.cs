using Application.Common;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Projects.Commands.UpdateAttraction;

public class UpdateAttractionCommand : IRequest<Result>
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public LocalizedText? Name { get; set; }
    public int? TimeInMinutes { get; set; }
    public IFormFile? Icon { get; set; }
    public bool RemoveIcon { get; set; }
}