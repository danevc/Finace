using System;

namespace Finace.Models;

/// <summary>
/// Транзакция
/// </summary>
public class Transaction
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Сумма
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Дата операции
    /// </summary>
    public DateTime OperationDate { get; set; }

    /// <summary>
    /// Счет списания
    /// </summary>
    public Account DebitAccount { get; set; }

    /// <summary>
    /// Счет зачисления 
    /// </summary>
    public Account TransferAccount { get; set; }

    /// <summary>
    /// доход, расход, перевод
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Тэг
    /// </summary>
    public Tag Tag { get; set; }
}
