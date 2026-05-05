using Application.Common;
using Application.Home.Dtos;
using Application.Home.Mapping;
using Domain.Interfaces;
using MediatR;

namespace Application.Home.Queries.GetHome;

public class GetHomeQueryHandler : IRequestHandler<GetHomeQuery, Result<HomeDto>>
{
    private readonly IVoiceOfCustomerRepository _voiceOfCustomerRepository;
    private readonly ISettingsRepository _settingsRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitRepository _unitRepository;

    public GetHomeQueryHandler(
        IVoiceOfCustomerRepository voiceOfCustomerRepository,
        ISettingsRepository settingsRepository,
        IProjectRepository projectRepository,
        IUnitRepository unitRepository)
    {
        _voiceOfCustomerRepository = voiceOfCustomerRepository;
        _settingsRepository = settingsRepository;
        _projectRepository = projectRepository;
        _unitRepository = unitRepository;
    }

    public async Task<Result<HomeDto>> Handle(
        GetHomeQuery request,
        CancellationToken cancellationToken)
    {
        var voices = await _voiceOfCustomerRepository.GetAllAsync(cancellationToken);
        var settings = await _settingsRepository.GetAsync(cancellationToken);
        var projectsCount = await _projectRepository.GetCount(cancellationToken);
        var unitsCount = await _unitRepository.GetCount(cancellationToken);

        var dto = new HomeDto
        {
            MediaUrl = settings?.HomeMediaUrl,
            VoiceOfCustomers = voices.Select(v => v.ToDto()).ToList(),
            Stats = new HomeStatsDto
            {
                YearsOfExperience = settings?.CompanyFoundingYear is null
                    ? 0
                    : DateTime.UtcNow.Year - settings.CompanyFoundingYear,
                TotalProjects = projectsCount,
                TotalUnits = unitsCount,
                NumberOfClients = settings?.NumberOfClients ?? 0
            }
        };

        return Result<HomeDto>.Success(dto);
    }
}