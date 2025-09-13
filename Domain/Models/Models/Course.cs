namespace Domain.Models;

/// <summary>
/// Таблица учебных дисциплин (курсов)
/// </summary>
public partial class Course
{
    /// <summary>
    /// Уникальный идентификатор курса (AI)
    /// </summary>
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    /// <summary>
    /// Внешний ключ к таблице преподавателей (кто ведет курс)
    /// </summary>
    public int TeacherId { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual Teacher Teacher { get; set; } = null!;
}
