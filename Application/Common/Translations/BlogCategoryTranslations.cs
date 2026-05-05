using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public static class BlogCategoryTranslations
{
    private static readonly Dictionary<BlogCategory, LocalizedText> _translations = new()
    {
        {
            BlogCategory.InvestmentGuide, new LocalizedText
            {
                English = "Investment Guide",
                Arabic = "دليل الاستثمار",
                Italian = "Guida agli Investimenti",
                Russian = "Руководство по инвестициям"
            }
        },
        {
            BlogCategory.Testimonials, new LocalizedText
            {
                English = "Testimonials",
                Arabic = "شهادات العملاء",
                Italian = "Testimonianze",
                Russian = "Отзывы"
            }
        }
    };

    public static LocalizedText GetTranslation(BlogCategory category)
        => _translations[category];
}