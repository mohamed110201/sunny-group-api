using Application.Inquiries.Dtos;
using Domain.Entities;
using Domain.Entities.inquiries;

namespace Application.Inquiries.Mapping;

public static class InquiryMappings
{
    public static InquiryDto ToDto(this Inquiry inquiry) => new()
    {
        Id = inquiry.Id,
        Name = inquiry.Name,
        FullPhoneNumber = inquiry.FullPhoneNumber,
        Email = inquiry.Email,
        Message = inquiry.Message,
        IsSeen = inquiry.IsSeen,
        SeenAt = inquiry.SeenAt,
        CreatedAt = inquiry.CreatedAt
    };
}