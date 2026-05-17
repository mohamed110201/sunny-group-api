using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public static class ProjectTypeTranslations
{
    private static readonly Dictionary<ProjectType, LocalizedText> _translations = new()
    {
        { ProjectType.Hotel, new LocalizedText { English = "Hotel", Arabic = "فندق", Italian = "Hotel", Russian = "Отель" } },
        { ProjectType.ResidentialApartments, new LocalizedText { English = "Residential Apartments", Arabic = "شقق سكنية", Italian = "Appartamenti Residenziali", Russian = "Жилые апартаменты" } },
        { ProjectType.HotelApartments, new LocalizedText { English = "Hotel Apartments", Arabic = "شقق فندقية", Italian = "Appartamenti Hotel", Russian = "Апартаменты отеля" } },
        { ProjectType.StandaloneVillas, new LocalizedText { English = "Standalone Villas", Arabic = "فيلات مستقلة", Italian = "Ville Indipendenti", Russian = "Отдельные виллы" } },
        { ProjectType.TownVilla, new LocalizedText { English = "Town Villa", Arabic = "تاون فيلا", Italian = "Town Villa", Russian = "Таун-вилла" } },
        { ProjectType.TwinVilla, new LocalizedText { English = "Twin Villa", Arabic = "تاون فيلا توين", Italian = "Twin Villa", Russian = "Твин-вилла" } }
    };

    public static LocalizedText GetTranslation(ProjectType type)
        => _translations[type];
}