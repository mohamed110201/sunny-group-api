using Application.Common;
using Application.Projects.Dtos;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Projects.Commands.AddProjectMedia;

public class AddProjectMediaCommand : IRequest<Result>
{
    public int ProjectId { get; set; }
    public List<ProjectMediaRequestDto> Images { get; set; } = [];
}