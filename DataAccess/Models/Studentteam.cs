using System;
using System.Collections.Generic;

namespace DataAccess.Models;

/// <summary>
/// Таблица для связи многие-ко-многим между Студентами и Командами
/// </summary>
public partial class Studentteam
{
    public int Id { get; set; }

    /// <summary>
    /// Внешний ключ к таблице студентов
    /// </summary>
    public int StudentId { get; set; }

    /// <summary>
    /// Внешний ключ к таблице команд
    /// </summary>
    public int TeamId { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Projectteam Team { get; set; } = null!;
}
