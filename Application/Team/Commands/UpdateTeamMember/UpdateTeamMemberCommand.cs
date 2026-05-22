using Application.Common;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Team.Commands.UpdateTeamMember;

public class UpdateTeamMemberCommand : IRequest<Result>
{
    public int Id { get; set; }
    public LocalizedText? Name { get; set; }
    public IFormFile? Image { get; set; }
    public bool RemoveImage { get; set; } = false;
    public LocalizedText? Position { get; set; }
    public LocalizedText? Department { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public SocialLinks? Links { get; set; }
}
