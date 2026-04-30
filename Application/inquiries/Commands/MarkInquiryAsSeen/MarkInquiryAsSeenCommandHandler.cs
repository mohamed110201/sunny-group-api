using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Inquiries.Commands.MarkInquiryAsSeen;

public class MarkInquiryAsSeenCommandHandler : IRequestHandler<MarkInquiryAsSeenCommand, Result>
{
    private readonly IInquiryRepository _inquiryRepository;
    private readonly IMemoryCache _cache;

    public MarkInquiryAsSeenCommandHandler(
        IInquiryRepository inquiryRepository,
        IMemoryCache cache)
    {
        _inquiryRepository = inquiryRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        MarkInquiryAsSeenCommand request,
        CancellationToken cancellationToken)
    {
        var inquiry = await _inquiryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (inquiry is null)
            return Result.Failure("Inquiry.NotFound", "Inquiry not found", 404);

        if (inquiry.IsSeen)
            return Result.Success();

        inquiry.IsSeen = true;
        inquiry.SeenAt = DateTime.UtcNow;

        await _inquiryRepository.UpdateAsync(inquiry, cancellationToken);
        _cache.Remove("inquiries-all");

        return Result.Success();
    }
}