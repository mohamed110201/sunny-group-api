using Application.Common.Translations;
using Application.Projects.Dtos;
using Application.Units.Mapping;
using Domain.Entities.Projects;
using Domain.Enums;

namespace Application.Projects.Mapping;

public static class ProjectMappings
{
    public static GetAllProjectsDto ToGetAllDto(this Project project) => new()
    {
        Id = project.Id,
        Name = project.Name,
        Description = project.Description,
        MetaTitle = project.MetaTitle,
        MetaDescription = project.MetaDescription,
        YearOfConstruction = project.YearOfConstruction,
        NumberOfVillas = project.NumberOfVillas,
        NumberOfApartments = project.NumberOfApartments,
        TotalUnits = project.TotalUnits,
        Area = project.Area,
        Location = project.Location,
        Status = ProjectStatusTranslations.GetTranslation(project.Status),
        Types = project.Types.Select(t => ProjectTypeTranslations.GetTranslation(t)).ToList(),
        PriceRange = project.PriceRange,
        CoverImage = project.Media
            .Where(m => m.Category == MediaCategory.Cover)
            .Select(m => m.ToDto())
            .FirstOrDefault()!,
        Icon = project.Media
            .Where(m => m.Category == MediaCategory.Icon)
            .Select(m => m.ToDto())
            .FirstOrDefault()!,
    };

    public static GetProjectByIdDto ToGetByIdDto(this Project project) => new()
    {
        Id = project.Id,
        Name = project.Name,
        Description = project.Description,
        MetaTitle = project.MetaTitle,
        MetaDescription = project.MetaDescription,
        YearOfConstruction = project.YearOfConstruction,
        NumberOfVillas = project.NumberOfVillas,
        NumberOfApartments = project.NumberOfApartments,
        TotalUnits = project.TotalUnits,
        Area = project.Area,
        Location = project.Location,
        Status = ProjectStatusTranslations.GetTranslation(project.Status),
        Types = project.Types.Select(t => ProjectTypeTranslations.GetTranslation(t)).ToList(),
        PriceRange = project.PriceRange,
        CoverImage = project.Media
            .Where(m => m.Category == MediaCategory.Cover)
            .Select(m => m.ToDto())
            .FirstOrDefault()!,
        Icon = project.Media
            .Where(m => m.Category == MediaCategory.Icon)
            .Select(m => m.ToDto())
            .FirstOrDefault()!,
        Media = project.Media.Select(m => m.ToDto()).ToList(),
        Attractions = project.Attractions
            .Select(a => a.ToDto())
            .ToList(),
        Units = project.Units.Select(u => u.ToGetAllDto()).ToList(),
        Documents = project.Documents.Select(d => d.ToDto()).ToList()
    };

    private static ProjectMediaResponseDto ToDto(this ProjectMedia media) => new()
    {
        Id = media.Id,
        Url = media.Url,
        Category = media.Category
    };
    private static AttractionResponseDto ToDto(this Attraction attraction) => new()
    {
        Id = attraction.Id,
        Name = attraction.Name,
        TimeInMinutes = attraction.TimeInMinutes,
        IconUrl = attraction.IconUrl
    };
    private static ProjectDocumentResponseDto ToDto(this ProjectDocument document) => new()
    {
        Id = document.Id,
        Url = document.Url,
        Category = document.Category
    };
}
