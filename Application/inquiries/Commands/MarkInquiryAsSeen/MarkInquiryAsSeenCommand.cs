using Application.Common;
using MediatR;

namespace Application.Inquiries.Commands.MarkInquiryAsSeen;

public record MarkInquiryAsSeenCommand(int Id) : IRequest<Result>;