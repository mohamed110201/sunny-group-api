using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public static class UnitTypeTranslations
{
    private static readonly Dictionary<UnitType, LocalizedText> _translations = new()
    {
        { UnitType.Studio, new LocalizedText { English = "Studio", Arabic = "ستوديو", Italian = "Monolocale", Russian = "Студия" } },
        { UnitType.OneBedroom, new LocalizedText { English = "One Bedroom", Arabic = "غرفة نوم واحدة", Italian = "Monolocale con Camera", Russian = "Одна спальня" } },
        { UnitType.TwoBedroom, new LocalizedText { English = "Two Bedroom", Arabic = "غرفتا نوم", Italian = "Due Camere", Russian = "Две спальни" } },
        { UnitType.ThreeBedroom, new LocalizedText { English = "Three Bedroom", Arabic = "ثلاث غرف نوم", Italian = "Tre Camere", Russian = "Три спальни" } },
        { UnitType.StandaloneVilla, new LocalizedText { English = "Standalone Villa", Arabic = "فيلا مستقلة", Italian = "Villa Indipendente", Russian = "Отдельная вилла" } },
        { UnitType.TwinVilla, new LocalizedText { English = "Twin Villa", Arabic = "تاون فيلا توين", Italian = "Twin Villa", Russian = "Твин-вилла" } },
        { UnitType.TownVilla, new LocalizedText { English = "Town Villa", Arabic = "تاون فيلا", Italian = "Town Villa", Russian = "Таун-вилла" } },
        { UnitType.Duplex, new LocalizedText { English = "Duplex", Arabic = "دوبلكس", Italian = "Duplex", Russian = "Дуплекс" } }
    };

    public static LocalizedText GetTranslation(UnitType type)
        => _translations[type];
}