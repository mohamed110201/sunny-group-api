using Application.Common;
using Application.Units.Dtos;
using MediatR;

namespace Application.Units.Commands.AddUnitMedia;

public class AddUnitMediaCommand : IRequest<Result>
{
    public int UnitId { get; set; }
    public int ProjectId { get; set; }
    public List<UnitMediaRequestDto> Images { get; set; } = [];
}