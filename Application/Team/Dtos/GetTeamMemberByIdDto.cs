using Domain.ValueObjects;

namespace Application.Team.Dtos;

public class GetTeamMemberByIdDto
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = new LocalizedText();
    public string? ImageUrl { get; set; }
    public LocalizedText Position { get; set; } = new LocalizedText();
    public LocalizedText Department { get; set; } = new LocalizedText();
    public string FullPhoneNumber { get; set; } = string.Empty;

    public string? Email { get; set; }
    public SocialLinks Links { get; set; } = new SocialLinks();
}
