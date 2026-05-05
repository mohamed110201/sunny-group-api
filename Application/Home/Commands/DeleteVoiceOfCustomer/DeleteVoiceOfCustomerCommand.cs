using Application.Common;
using MediatR;

namespace Application.Home.Commands.DeleteVoiceOfCustomer;

public record DeleteVoiceOfCustomerCommand(int Id) : IRequest<Result>;