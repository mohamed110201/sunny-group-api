using Application.Common;
using MediatR;

namespace Application.Home.Commands.UpdateSettings;

public class UpdateSettingsCommand : IRequest<Result>
{
    public int CompanyFoundingYear { get; set; }
    public int NumberOfClients { get; set; }
}