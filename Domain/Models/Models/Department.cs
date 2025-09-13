namespace Domain.Models;

/// <summary>
/// Таблица кафедр колледжа
/// </summary>
public partial class Department
{
    /// <summary>
    /// Уникальный идентификатор кафедры (AI)
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    /// Название кафедры
    /// </summary>
    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
