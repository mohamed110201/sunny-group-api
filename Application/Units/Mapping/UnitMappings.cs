using Application.Common.Translations;
using Application.Projects.Dtos;
using Application.Units.Dtos;
using Domain.Entities.Projects;
using Domain.Entities.Units;
using Domain.Enums;

namespace Application.Units.Mapping;

public static class UnitMappings
{
    public static GetAllUnitsDto ToGetAllDto(this Unit unit) => new()
    {
        Id = unit.Id,
        Name = unit.Name,
        Area = unit.Area,
        MetaTitle = unit.MetaTitle,
        MetaDescription = unit.MetaDescription,
        Rooms = unit.Rooms,
        NumberOfBathrooms = unit.NumberOfBathrooms,
        Status = UnitStatusTranslations.GetTranslation(unit.Status),
        Type = UnitTypeTranslations.GetTranslation(unit.Type),   
        DeliveryCondition = DeliveryConditionTranslations.GetTranslation(unit.DeliveryCondition),
        Price = unit.Price,
        Stock = unit.Stock,
        ROI = unit.ROI,
        CoverImage = unit.Media
            .Where(m => m.Category == MediaCategory.Cover)
            .Select(m => m.ToResponseDto())
            .FirstOrDefault(),
        ProjectIconUrl = unit.Project.Media
            .FirstOrDefault(m => m.Category == MediaCategory.Icon)?.Url,
        ProjectMapUrl = unit.Project.MapUrl
    };

    public static GetUnitByIdDto ToGetByIdDto(this Unit unit) => new()
    {
        Id = unit.Id,
        Name = unit.Name,
        Area = unit.Area,
        MetaTitle = unit.MetaTitle,
        MetaDescription = unit.MetaDescription,
        Rooms = unit.Rooms,
        NumberOfBathrooms = unit.NumberOfBathrooms,
        Status = UnitStatusTranslations.GetTranslation(unit.Status),
        Type = UnitTypeTranslations.GetTranslation(unit.Type),
        DeliveryCondition = DeliveryConditionTranslations.GetTranslation(unit.DeliveryCondition),
        Price = unit.Price,
        Stock = unit.Stock,
        PaymentPlan = unit.PaymentPlan,
        ROI = unit.ROI,
        PaidToStartEarn = unit.PaidToStartEarn,
        MonthlyRent = unit.MonthlyRent,
        YearsToEarn = unit.YearsToEarn,
        PaidToStartEarnPercentage = unit.PaidToStartEarnPercentage,
        AnnualYield = unit.AnnualYield,
        CoverImage = unit.Media
            .Where(m => m.Category == MediaCategory.Cover)
            .Select(m => m.ToResponseDto())
            .FirstOrDefault(),
        GalleryImages = unit.Media
            .Where(m => m.Category == MediaCategory.Gallery)
            .Select(m => m.ToResponseDto())
            .ToList(),
        InventoryImages = unit.Media
            .Where(m => m.Category == MediaCategory.Inventory)
            .Select(m => m.ToResponseDto())
            .ToList(),
        Documents = unit.Documents.Select(d => d.ToDto()).ToList(),
        ProjectIconUrl = unit.Project.Media
        .FirstOrDefault(m => m.Category == MediaCategory.Icon)?.Url,
        ProjectMapUrl = unit.Project.MapUrl
    };

    private static UnitMediaResponseDto ToResponseDto(this UnitMedia media) => new()
    {
        Id = media.Id,
        Url = media.Url,
        Category = media.Category,
        SubCategory = media.SubCategory
    };
    private static UnitDocumentResponseDto ToDto(this UnitDocument document) => new()
    {
        Id = document.Id,
        Url = document.Url,
        Category = document.Category
    };
}