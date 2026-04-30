using Application.Common;
using Domain.Entities.inquiries;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Inquiries.Commands.CreateInquiry;

public class CreateInquiryCommandHandler : IRequestHandler<CreateInquiryCommand, Result>
{
    private readonly IInquiryRepository _inquiryRepository;
    private readonly IMemoryCache _cache;

    public CreateInquiryCommandHandler(
        IInquiryRepository inquiryRepository,
        IMemoryCache cache)
    {
        _inquiryRepository = inquiryRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        CreateInquiryCommand request,
        CancellationToken cancellationToken)
    {
        var inquiry = new Inquiry
        {
            Name = request.Name,
            CountryCode = request.CountryCode,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };

        await _inquiryRepository.AddAsync(inquiry, cancellationToken);

        _cache.Remove("inquiries-all");

        return Result.Success();
    }
}