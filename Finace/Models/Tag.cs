using System;

namespace Finace.Models;

/// <summary>
/// Тэг
/// </summary>
public class Tag
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
}
