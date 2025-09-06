using System;
using System.Collections.Generic;

namespace DataAccess.Models;

/// <summary>
/// Таблица оценок, выставленных преподавателями за сданные работы
/// </summary>
public partial class Grade
{
    /// <summary>
    /// Уникальный идентификатор оценки (AI)
    /// </summary>
    public int GradeId { get; set; }

    /// <summary>
    /// Внешний ключ к таблице сданных работ (за какую работу оценка)
    /// </summary>
    public int SubmissionId { get; set; }

    /// <summary>
    /// Внешний ключ к таблице преподавателей (кто оценил)
    /// </summary>
    public int TeacherId { get; set; }

    /// <summary>
    /// Количество набранных баллов
    /// </summary>
    public int Score { get; set; }

    public string? Feedback { get; set; }

    public DateOnly GradingDate { get; set; }

    public virtual Submission Submission { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
