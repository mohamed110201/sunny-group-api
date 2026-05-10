using Application.Common;
using MediatR;

namespace Application.Inquiries.Commands.CreateInquiry;

public class CreateInquiryCommand : IRequest<Result>
{
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? PageSlug { get; set; }

}