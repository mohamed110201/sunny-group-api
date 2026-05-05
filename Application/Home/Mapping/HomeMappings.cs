using Application.Home.Dtos;
using Domain.Entities.Home;

namespace Application.Home.Mapping;

public static class HomeMapping
{
    public static VoiceOfCustomerDto ToDto(this VoiceOfCustomer v) => new()
    {
        CustomerName = v.CustomerName,
        ProjectName = v.Project.Name,
        Comment = v.Comment
    };
}