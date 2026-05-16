using Application.Common;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Home.Commands.UpdateVoiceOfCustomer;

public class UpdateVoiceOfCustomerCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public int? ProjectId { get; set; }
    public LocalizedText? Comment { get; set; }
    public IFormFile? Image { get; set; }
    public bool RemoveImage { get; set; }
}