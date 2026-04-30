using Application.Common.Translations;
using Application.Units.Dtos;
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
        Rooms = unit.Rooms,
        Status = StatusTranslations.GetTranslation(unit.Status),
        Type = UnitTypeTranslations.GetTranslation(unit.Type),        Price = unit.Price,
        Stock = unit.Stock,
        ROI = unit.ROI,
        CoverImage = unit.Media
            .Where(m => m.Category == MediaCategory.Cover)
            .Select(m => m.ToResponseDto())
            .FirstOrDefault()
    };

    public static GetUnitByIdDto ToGetByIdDto(this Unit unit) => new()
    {
        Id = unit.Id,
        Name = unit.Name,
        Area = unit.Area,
        Rooms = unit.Rooms,
        Status = StatusTranslations.GetTranslation(unit.Status),
        Type = UnitTypeTranslations.GetTranslation(unit.Type),
        Price = unit.Price,
        Stock = unit.Stock,
        PaymentPlan = unit.PaymentPlan,
        ROI = unit.ROI,
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
            .ToList()
    };

    private static UnitMediaResponseDto ToResponseDto(this UnitMedia media) => new()
    {
        Id = media.Id,
        Url = media.Url,
        Category = media.Category,
        SubCategory = media.SubCategory
    };
}