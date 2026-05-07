using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public class DeliveryConditionTranslations
{
    private static readonly Dictionary<DeliveryCondition, LocalizedText> _translations = new()
    {
        {
            DeliveryCondition.Unfinished, new LocalizedText
            {
                English = "Unfinished",
                Arabic = "قيد الإنشاء",
                Italian = "In Costruzione",
                Russian = "В строительстве"
            }
        },
        {
            DeliveryCondition.FullyFinished, new LocalizedText
            {
                English = "Finished",
                Arabic = "مكتمل",
                Italian = "Finita",
                Russian = "Законченный"
            }
        }
    };
        public static LocalizedText GetTranslation(DeliveryCondition condition)
        => _translations[condition];
}
