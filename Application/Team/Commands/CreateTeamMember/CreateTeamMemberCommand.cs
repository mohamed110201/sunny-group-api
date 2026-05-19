using Application.Common;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Team.Commands.CreateTeamMember;

public class CreateTeamMemberCommand : IRequest<Result>
{
    public LocalizedText Name { get; set; } = new LocalizedText();
    public IFormFile? Image { get; set; }
    public LocalizedText Position { get; set; } = new LocalizedText();
    public LocalizedText Department { get; set; } = new LocalizedText();
    public string? CountryCode { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public SocialLinks Links { get; set; } = new SocialLinks();
}
