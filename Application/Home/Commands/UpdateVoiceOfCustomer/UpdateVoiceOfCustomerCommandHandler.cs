using Application.Common;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Home.Commands.UpdateVoiceOfCustomer;

public class UpdateVoiceOfCustomerCommandHandler : IRequestHandler<UpdateVoiceOfCustomerCommand, Result>
{
    private readonly IVoiceOfCustomerRepository _voiceOfCustomerRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;

    private readonly IMemoryCache _cache;

    public UpdateVoiceOfCustomerCommandHandler(
        IVoiceOfCustomerRepository voiceOfCustomerRepository,
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage,

        IMemoryCache cache)
    {
        _voiceOfCustomerRepository = voiceOfCustomerRepository;
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;

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

        if (request.ProjectId.HasValue)
        {
            var projectExists = await _projectRepository
                .ExistsAsync(request.ProjectId.Value, cancellationToken);

            if (!projectExists)
                return Result.Failure("Project.NotFound", "Project not found", 404);

            voiceOfCustomer.ProjectId = request.ProjectId.Value;
        }

        if (request.CustomerName != null)
            voiceOfCustomer.CustomerName = request.CustomerName;

        if (request.Comment != null)
        {
            voiceOfCustomer.Comment.English = request.Comment.English ?? voiceOfCustomer.Comment.English;
            voiceOfCustomer.Comment.Arabic = request.Comment.Arabic ?? voiceOfCustomer.Comment.Arabic;
            voiceOfCustomer.Comment.Italian = request.Comment.Italian ?? voiceOfCustomer.Comment.Italian;
            voiceOfCustomer.Comment.Russian = request.Comment.Russian ?? voiceOfCustomer.Comment.Russian;
        }
        if (request.RemoveImage)
        {
            if (!string.IsNullOrEmpty(voiceOfCustomer.ImageUrl))
                await _mediaStorage.DeleteAsync(voiceOfCustomer.ImageUrl, cancellationToken);
            voiceOfCustomer.ImageUrl = null;
        }
        else if (request.Image != null)
        {
            if (!string.IsNullOrEmpty(voiceOfCustomer.ImageUrl))
                await _mediaStorage.DeleteAsync(voiceOfCustomer.ImageUrl, cancellationToken);

            voiceOfCustomer.ImageUrl = await _mediaStorage.SaveAsync(
                request.Image,
                "voice-of-customers",
                cancellationToken);
        }
        await _voiceOfCustomerRepository.UpdateAsync(voiceOfCustomer, cancellationToken);
        _cache.Remove("home");

        return Result.Success();
    }
}