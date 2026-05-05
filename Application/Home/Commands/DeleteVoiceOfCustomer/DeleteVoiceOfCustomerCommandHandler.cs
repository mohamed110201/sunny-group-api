using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Home.Commands.DeleteVoiceOfCustomer;

public class DeleteVoiceOfCustomerCommandHandler : IRequestHandler<DeleteVoiceOfCustomerCommand, Result>
{
    private readonly IVoiceOfCustomerRepository _voiceOfCustomerRepository;
    private readonly IMemoryCache _cache;

    public DeleteVoiceOfCustomerCommandHandler(
        IVoiceOfCustomerRepository voiceOfCustomerRepository,
        IMemoryCache cache)
    {
        _voiceOfCustomerRepository = voiceOfCustomerRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        DeleteVoiceOfCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var voiceOfCustomer = await _voiceOfCustomerRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (voiceOfCustomer is null)
            return Result.Failure("VoiceOfCustomer.NotFound", "Voice of customer not found", 404);

        await _voiceOfCustomerRepository.DeleteAsync(voiceOfCustomer, cancellationToken);
        _cache.Remove("home");

        return Result.Success();
    }
}