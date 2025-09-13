namespace Domain.Models;

/// <summary>
/// Таблица команд, создаваемых для работы над проектами
/// </summary>
public partial class Projectteam
{
    /// <summary>
    /// Уникальный идентификатор команды (AI)
    /// </summary>
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    /// <summary>
    /// Внешний ключ к таблице проектов (над каким проектом работает команда)
    /// </summary>
    public int ProjectId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Studentteam> Studentteams { get; set; } = new List<Studentteam>();

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
