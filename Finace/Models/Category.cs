using System;

namespace Finace.Models;

/// <summary>
/// Категория
/// </summary>
public class Category
{
    /// <summary>
    /// Уникальный идентификатор 
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Родительская категория
    /// </summary>
    public Category ParentCategory { get; set; }

    /// <summary>
    /// Активность категории
    /// </summary>
    public bool IsActive { get; set; }
}


