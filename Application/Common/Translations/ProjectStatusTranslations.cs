using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Translations;

public static class ProjectStatusTranslations
{
    private static readonly Dictionary<ProjectStatus, LocalizedText> _translations = new()
    {
        { ProjectStatus.Phase1Delivered, new LocalizedText { English = "Phase 1 Delivered", Arabic = "تم تسليم المرحلة الأولى", Italian = "Fase 1 Consegnata", Russian = "Фаза 1 Сдана" } },
        { ProjectStatus.Phase2Ready, new LocalizedText { English = "Phase 2 Ready", Arabic = "المرحلة الثانية جاهزة", Italian = "Fase 2 Pronta", Russian = "Фаза 2 Готова" } },
        { ProjectStatus.Phase2ReadySoon, new LocalizedText { English = "Phase 2 Ready Soon", Arabic = "المرحلة الثانية قريباً", Italian = "Fase 2 Presto Pronta", Russian = "Фаза 2 Скоро Будет Готова" } },
        { ProjectStatus.UnderConstruction, new LocalizedText { English = "Under Construction", Arabic = "قيد الإنشاء", Italian = "In Costruzione", Russian = "В строительстве" } },
        { ProjectStatus.UnderFinishing, new LocalizedText { English = "Under Finishing", Arabic = "قيد التشطيب", Italian = "In Finitura", Russian = "В отделке" } },
        { ProjectStatus.ReadyToMove, new LocalizedText { English = "Ready to Move", Arabic = "جاهز للسكن", Italian = "Pronto per Trasferirsi", Russian = "Готов к заселению" } },
        { ProjectStatus.LastPhaseReadySoon, new LocalizedText { English = "Last Phase Ready Soon", Arabic = "المرحلة الأخيرة قريباً", Italian = "Ultima Fase Presto Pronta", Russian = "Последняя фаза скоро будет готова" } },
        { ProjectStatus.LaunchingSoon, new LocalizedText { English = "Launching Soon", Arabic = "الإطلاق قريباً", Italian = "Lancio Imminente", Russian = "Скоро запуск" } }
    };

    public static LocalizedText GetTranslation(ProjectStatus status)
        => _translations[status];
}