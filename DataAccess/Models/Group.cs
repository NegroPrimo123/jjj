using System;
using System.Collections.Generic;

namespace DataAccess.Models;

/// <summary>
/// Таблица учебных групп колледжа
/// </summary>
public partial class Group
{
    /// <summary>
    /// Уникальный идентификатор группы (AI)
    /// </summary>
    public int GroupId { get; set; }

    /// <summary>
    /// Название группы (например, ИСП-421)
    /// </summary>
    public string GroupName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
