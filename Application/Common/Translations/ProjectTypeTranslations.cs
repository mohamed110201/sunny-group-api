using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public class ProjectTypeTranslations
{
    private static readonly Dictionary<ProjectType, LocalizedText> _translations = new()
    {
        {
            ProjectType.Apartments, new LocalizedText
            {
                English = "Apartments",
                Arabic = "شقق",
                Italian = "Appartamenti",
                Russian = "Квартиры"
            }
        },
        {
            ProjectType.Hotels, new LocalizedText
            {
                English = "Hotels",
                Arabic = "فنادق",
                Italian = "Hotel",
                Russian = "Отели"
            }
        },
        {
            ProjectType.Studios, new LocalizedText
            {
                English = "Studios",
                Arabic = "استوديو",
                Italian = "Studios",
                Russian = "Студии"
            }
        },
        {
            ProjectType.StandaloneVillas, new LocalizedText
            {
                English = "StandaloneVillas",
                Arabic = "فلل مستقلة",
                Italian = "Ville indipendenti",
                Russian = "Отдельно стоящие виллы"
            }
        }
    };
    
    public static LocalizedText GetTranslation(ProjectType type)
        => _translations[type];
}