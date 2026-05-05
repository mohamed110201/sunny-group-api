using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Home.Commands.UpdateHomeMedia;

public class UpdateHomeMediaCommand : IRequest<Result>
{
    public IFormFile Media { get; set; } = null!;
}