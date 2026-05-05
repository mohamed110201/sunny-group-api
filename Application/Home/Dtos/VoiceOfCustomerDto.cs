using Domain.ValueObjects;

namespace Application.Home.Dtos;

public class VoiceOfCustomerDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public LocalizedText ProjectName { get; set; } = null!;
    public LocalizedText Comment { get; set; } = null!;
}