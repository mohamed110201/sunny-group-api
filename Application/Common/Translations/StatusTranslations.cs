using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public class StatusTranslations
{
    private static readonly Dictionary<ProjectStatus, LocalizedText> _translations = new()
    {
        {
            ProjectStatus.UnderFinishing, new LocalizedText
            {
                English = "Under Finishing",
                Arabic = "قيد الإنشاء",
                Italian = "In Costruzione",
                Russian = "В строительстве"
            }
        },
        {
            ProjectStatus.Finished, new LocalizedText
            {
                English = "Finished",
                Arabic = "مكتمل",
                Italian = "Finita",
                Russian = "Законченный"
            }
        },
        {
            ProjectStatus.ReadyToMove, new LocalizedText
            {
                English = "Ready To Move",
                Arabic = "جاهز للسكن",
                Italian = "Pronto per il trasloco",
                Russian = "Готов к переезду"
            }
        }
    };

    public static LocalizedText GetTranslation(ProjectStatus status)
        => _translations[status];
}