using System;

namespace Finace.Models;

/// <summary>
/// Пользователь
/// </summary>s
public class User
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
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }

}
