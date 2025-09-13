namespace Domain.Models;

/// <summary>
/// Таблица проектов, назначаемых по дисциплинам
/// </summary>
public partial class Project
{
    /// <summary>
    /// Уникальный идентификатор проекта (AI)
    /// </summary>
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string? Description { get; set; }

    /// <summary>
    /// Внешний ключ к таблице курсов (к какой дисциплине относится проект)
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Максимально возможный балл за проект
    /// </summary>
    public int MaxScore { get; set; }

    /// <summary>
    /// Крайний срок сдачи проекта
    /// </summary>
    public DateOnly Deadline { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Projectteam> Projectteams { get; set; } = new List<Projectteam>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
