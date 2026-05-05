namespace Application.Home.Dtos;

public class HomeDto
{
    public string? MediaUrl { get; set; }
    public List<VoiceOfCustomerDto> VoiceOfCustomers { get; set; } = [];
    public HomeStatsDto Stats { get; set; } = null!;
}