using System;

namespace Finace.Models;

/// <summary>
/// Счёт
/// </summary>
public class Account
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public long AccountId { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Тип
    /// </summary>
    public string Type { get; set; }
}