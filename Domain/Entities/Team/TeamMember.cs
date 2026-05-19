using Domain.ValueObjects;

namespace Domain.Entities.Team;

public class TeamMember
{
    public int Id { get; set; }
    public LocalizedText Name { get; set; } = new LocalizedText();
    public string? ImageUrl { get; set; }
    public LocalizedText Position { get; set; } = new LocalizedText();
    public LocalizedText Department { get; set; } = new LocalizedText();
    public string CountryCode { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string FullPhoneNumber => $"{CountryCode}{PhoneNumber}";

    public string? Email { get; set; }
    public SocialLinks Links { get; set; } = new SocialLinks();
}
