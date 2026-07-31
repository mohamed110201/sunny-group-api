using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public static class UnitStatusTranslations
{
    private static readonly Dictionary<UnitStatus, LocalizedText> _translations = new()
    {
        {
            UnitStatus.FullyFinished,
            new LocalizedText
            {
                English = "Fully Finished",
                Arabic = "تشطيب كامل",
                Italian = "Completamente Rifinito",
                Russian = "Полностью отделан"
            }
        },
        {
            UnitStatus.Unfinished,
            new LocalizedText
            {
                English = "Unfinished",
                Arabic = "غير مشطب",
                Italian = "Non Rifinito",
                Russian = "Без отделки"
            }
        },
        {
            UnitStatus.HalfFinished,
            new LocalizedText
            {
                English = "Half Finished",
                Arabic = "نصف تشطيب",
                Italian = "Parzialmente Rifinito",
                Russian = "Частично отделан"
            }
        },
        {
            UnitStatus.FullyFurnished,
            new LocalizedText
            {
                English = "Fully Furnished",
                Arabic = "مفروش بالكامل",
                Italian = "Completamente Arredato",
                Russian = "Полностью меблирован"
            }
        },
    };

    public static LocalizedText GetTranslation(UnitStatus status)
        => _translations[status];
}