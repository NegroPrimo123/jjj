using System;
using System.Collections.Generic;

namespace DataAccess.Models;

/// <summary>
/// Таблица этапов (задач) в рамках проекта
/// </summary>
public partial class Task
{
    /// <summary>
    /// Уникальный идентификатор этапа (AI)
    /// </summary>
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    /// <summary>
    /// Внешний ключ к таблице проектов (в рамках какого проекта этап)
    /// </summary>
    public int ProjectId { get; set; }

    public DateOnly TaskDeadline { get; set; }

    /// <summary>
    /// Максимальный балл за данный этап
    /// </summary>
    public int TaskMaxScore { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
