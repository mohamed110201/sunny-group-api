using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Home;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Home.Commands.CreateVoiceOfCustomer;

public class CreateVoiceOfCustomerCommandHandler : IRequestHandler<CreateVoiceOfCustomerCommand, Result>
{
    private readonly IVoiceOfCustomerRepository _voiceOfCustomerRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public CreateVoiceOfCustomerCommandHandler(
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
        CreateVoiceOfCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var projectExists = await _projectRepository
            .ExistsAsync(request.ProjectId, cancellationToken);
        string? imageUrl = null;
        
        if (!projectExists)
            return Result.Failure("Project.NotFound", "Project not found", 404);
        if (request.Image != null)
             imageUrl = await _mediaStorage.SaveAsync(
                request.Image,
                "voice-of-customers",
                cancellationToken);
        var voiceOfCustomer = new VoiceOfCustomer
        {
            CustomerName = request.CustomerName,
            ProjectId = request.ProjectId,
            Comment = request.Comment,
            ImageUrl = imageUrl

        };

        await _voiceOfCustomerRepository.AddAsync(voiceOfCustomer, cancellationToken);
        _cache.Remove("home");

        return Result.Success();
    }
}