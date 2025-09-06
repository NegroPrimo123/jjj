using System;
using System.Collections.Generic;

namespace DataAccess.Models;

/// <summary>
/// Таблица сданных студенческих работ (по этапам)
/// </summary>
public partial class Submission
{
    /// <summary>
    /// Уникальный идентификатор отправки (AI)
    /// </summary>
    public int SubmissionId { get; set; }

    /// <summary>
    /// Внешний ключ к таблице команд (какая команда сдала)
    /// </summary>
    public int TeamId { get; set; }

    /// <summary>
    /// Внешний ключ к таблице этапов (какой этап сдан)
    /// </summary>
    public int TaskId { get; set; }

    public DateTime SubmissionDate { get; set; }

    public string? FilePath { get; set; }

    /// <summary>
    /// Текущий статус проверки работы
    /// </summary>
    public string Status { get; set; } = null!;

    public virtual Grade? Grade { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual Projectteam Team { get; set; } = null!;
}
