using Application.Common;
using Application.Projects.Dtos;
using MediatR;

namespace Application.Projects.Commands.AddAttraction;

public class AddAttractionCommand : IRequest<Result>
{
    public int ProjectId { get; set; }
    public List<AttractionRequestDto> Attractions { get; set; } = [];
}