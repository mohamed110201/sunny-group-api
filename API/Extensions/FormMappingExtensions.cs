using Application.Projects.Commands.AddProjectMedia;
using Application.Projects.Dtos;
using Application.Units.Commands.AddUnitMedia;
using Application.Units.Dtos;
using Domain.Enums;

namespace API.Extensions;

public static class FormMappingExtensions
{
    public static AddProjectMediaCommand ToCommand(this AddProjectMediaForm form, int projectId)
    {
        return new AddProjectMediaCommand
        {
            ProjectId = projectId,
            Images = form.Images
                .Select((img, i) => new ProjectMediaRequestDto
                {
                    Image = img,
                    Category = form.Categories[i]
                })
                .ToList()
        };
    }

    public static AddUnitMediaCommand ToCommand(this AddUnitMediaForm form, int projectId, int unitId)
    {
        return new AddUnitMediaCommand
        {
            UnitId = unitId,
            ProjectId = projectId,
            Images = form.Images
                .Select((img, i) => new UnitMediaRequestDto
                {
                    Image = img,
                    Category = form.Categories[i],
                    SubCategory = form.SubCategories[i]
                })
                .ToList()
        };
    }
}