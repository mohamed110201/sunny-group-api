using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Home.Commands.UpdateVoiceOfCustomer;

public class UpdateVoiceOfCustomerCommandHandler : IRequestHandler<UpdateVoiceOfCustomerCommand, Result>
{
    private readonly IVoiceOfCustomerRepository _voiceOfCustomerRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMemoryCache _cache;

    public UpdateVoiceOfCustomerCommandHandler(
        IVoiceOfCustomerRepository voiceOfCustomerRepository,
        IProjectRepository projectRepository,
        IMemoryCache cache)
    {
        _voiceOfCustomerRepository = voiceOfCustomerRepository;
        _projectRepository = projectRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        UpdateVoiceOfCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var voiceOfCustomer = await _voiceOfCustomerRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (voiceOfCustomer is null)
            return Result.Failure("VoiceOfCustomer.NotFound", "Voice of customer not found", 404);

        var projectExists = await _projectRepository
            .ExistsAsync(request.ProjectId, cancellationToken);

        if (!projectExists)
            return Result.Failure("Project.NotFound", "Project not found", 404);

        voiceOfCustomer.CustomerName = request.CustomerName;
        voiceOfCustomer.ProjectId = request.ProjectId;
        voiceOfCustomer.Comment.English = request.Comment.English;
        voiceOfCustomer.Comment.Arabic = request.Comment.Arabic ?? voiceOfCustomer.Comment.Arabic;
        voiceOfCustomer.Comment.Italian = request.Comment.Italian ?? voiceOfCustomer.Comment.Italian;
        voiceOfCustomer.Comment.Russian = request.Comment.Russian ?? voiceOfCustomer.Comment.Russian;

        await _voiceOfCustomerRepository.UpdateAsync(voiceOfCustomer, cancellationToken);
        _cache.Remove("home");

        return Result.Success();
    }
}