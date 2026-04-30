using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public class UnitTypeTranslations
{
    private static readonly Dictionary<UnitType, LocalizedText> _translations = new()
    {
    {
        UnitType.Apartment, new LocalizedText
        {
            English = "Apartment",
            Arabic = "شقة",
            Italian = "Appartamento",
            Russian = "Квартира"
        }
    },
    {
        UnitType.Villa, new LocalizedText
        {
            English = "Villa",
            Arabic = "فيلا",
            Italian = "Villa",
            Russian = "Вилла"
        }
    },
    {
        UnitType.Studio, new LocalizedText
        {
            English = "Studio",
            Arabic = "ستوديو",
            Italian = "Monolocale",
            Russian = "Студия"
        }
    }
};

public static LocalizedText GetTranslation(UnitType type)
    => _translations[type];
}