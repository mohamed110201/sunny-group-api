namespace Application.Inquiries.Dtos;

public class InquiryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FullPhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsSeen { get; set; }
    public DateTime? SeenAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PageSlug { get; set; } = string.Empty;

}