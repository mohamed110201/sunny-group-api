using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public static class UnitStatusTranslations
{
    private static readonly Dictionary<UnitStatus, LocalizedText> _translations = new()
    {
        { UnitStatus.ReadyToMove, new LocalizedText { English = "Ready to Move", Arabic = "جاهز للسكن", Italian = "Pronto per Trasferirsi", Russian = "Готов к заселению" } },
        { UnitStatus.UnderFinishing, new LocalizedText { English = "Under Finishing", Arabic = "قيد التشطيب", Italian = "In Finitura", Russian = "В отделке" } },
        { UnitStatus.UnderConstruction, new LocalizedText { English = "Under Construction", Arabic = "قيد الإنشاء", Italian = "In Costruzione", Russian = "В строительстве" } },
        { UnitStatus.DeliverySoon, new LocalizedText { English = "Delivery Soon", Arabic = "التسليم قريباً", Italian = "Consegna Imminente", Russian = "Скоро доставка" } }
    };

    public static LocalizedText GetTranslation(UnitStatus status)
        => _translations[status];
}