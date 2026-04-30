using Application.Common;
using Application.Inquiries.Dtos;
using Application.Inquiries.Mapping;
using Domain.Interfaces;
using MediatR;

namespace Application.Inquiries.Queries.GetAllInquiries;

public class GetAllInquiriesQueryHandler : IRequestHandler<GetAllInquiriesQuery, Result<List<InquiryDto>>>
{
    private readonly IInquiryRepository _inquiryRepository;

    public GetAllInquiriesQueryHandler(IInquiryRepository inquiryRepository)
    {
        _inquiryRepository = inquiryRepository;
    }

    public async Task<Result<List<InquiryDto>>> Handle(
        GetAllInquiriesQuery request,
        CancellationToken cancellationToken)
    {
        var inquiries = await _inquiryRepository.GetAllAsync(cancellationToken);
        return Result<List<InquiryDto>>.Success(
            inquiries.Select(i => i.ToDto()).ToList());
    }
}