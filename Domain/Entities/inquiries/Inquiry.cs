namespace Domain.Entities.inquiries;

public class Inquiry
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string FullPhoneNumber => $"{CountryCode}{PhoneNumber}";
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsSeen { get; set; }
    public DateTime? SeenAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PageSlug { get; set; } = string.Empty;

}