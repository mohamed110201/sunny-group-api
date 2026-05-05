using Application.Common;
using Domain.ValueObjects;
using MediatR;

namespace Application.Home.Commands.UpdateVoiceOfCustomer;

public class UpdateVoiceOfCustomerCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public LocalizedText Comment { get; set; } = null!;
}