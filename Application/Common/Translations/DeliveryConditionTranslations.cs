using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public static class DeliveryConditionTranslations
{
    private static readonly Dictionary<DeliveryCondition, LocalizedText> _translations = new()
    {
        { DeliveryCondition.FullyFinished, new LocalizedText { English = "Fully Finished", Arabic = "تشطيب كامل", Italian = "Completamente Rifinito", Russian = "Полностью отделан" } },
        { DeliveryCondition.HalfFinished, new LocalizedText { English = "Half Finished", Arabic = "نصف تشطيب", Italian = "Parzialmente Rifinito", Russian = "Частично отделан" } },
        { DeliveryCondition.Unfinished, new LocalizedText { English = "Unfinished", Arabic = "غير مشطب", Italian = "Non Rifinito", Russian = "Без отделки" } },
        { DeliveryCondition.FullyFurnished, new LocalizedText { English = "Fully Furnished", Arabic = "مفروش بالكامل", Italian = "Completamente Arredato", Russian = "Полностью меблирован" } }
    };

    public static LocalizedText GetTranslation(DeliveryCondition condition)
        => _translations[condition];
}