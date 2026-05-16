using Application.Common;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Home.Commands.CreateVoiceOfCustomer;

public class CreateVoiceOfCustomerCommand : IRequest<Result>
{
    public string CustomerName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public LocalizedText Comment { get; set; } = null!;
    public IFormFile? Image { get; set; }
}