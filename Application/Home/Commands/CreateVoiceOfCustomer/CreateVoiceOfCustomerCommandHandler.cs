using Application.Common;
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
    private readonly IMemoryCache _cache;

    public CreateVoiceOfCustomerCommandHandler(
        IVoiceOfCustomerRepository voiceOfCustomerRepository,
        IProjectRepository projectRepository,
        IMemoryCache cache)
    {
        _voiceOfCustomerRepository = voiceOfCustomerRepository;
        _projectRepository = projectRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        CreateVoiceOfCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var projectExists = await _projectRepository
            .ExistsAsync(request.ProjectId, cancellationToken);

        if (!projectExists)
            return Result.Failure("Project.NotFound", "Project not found", 404);

        var voiceOfCustomer = new VoiceOfCustomer
        {
            CustomerName = request.CustomerName,
            ProjectId = request.ProjectId,
            Comment = request.Comment
        };

        await _voiceOfCustomerRepository.AddAsync(voiceOfCustomer, cancellationToken);
        _cache.Remove("home");

        return Result.Success();
    }
}