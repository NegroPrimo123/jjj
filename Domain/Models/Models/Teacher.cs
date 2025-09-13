namespace Domain.Models;

/// <summary>
/// Таблица преподавателей
/// </summary>
public partial class Teacher
{
    /// <summary>
    /// Уникальный идентификатор преподавателя (AI)
    /// </summary>
    public int TeacherId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    /// <summary>
    /// Внешний ключ к таблице кафедр
    /// </summary>
    public int DepartmentId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
