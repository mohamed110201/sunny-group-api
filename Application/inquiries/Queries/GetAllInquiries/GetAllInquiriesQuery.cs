using Application.Common;
using Application.Common.Behaviors;
using Application.Inquiries.Dtos;
using MediatR;

namespace Application.Inquiries.Queries.GetAllInquiries;

public record GetAllInquiriesQuery : IRequest<Result<List<InquiryDto>>>, ICacheable
{
    public string CacheKey => "inquiries-all";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(5);
}