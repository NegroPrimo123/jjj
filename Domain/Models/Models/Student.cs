namespace Domain.Models;

/// <summary>
/// Таблица студентов
/// </summary>
public partial class Student
{
    /// <summary>
    /// Уникальный идентификатор студента (AI)
    /// </summary>
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    /// <summary>
    /// Внешний ключ к таблице учебных групп
    /// </summary>
    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Studentteam> Studentteams { get; set; } = new List<Studentteam>();
}
