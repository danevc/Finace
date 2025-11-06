namespace Finace.Models
{
    /// <summary>
    /// Представляет запись о финансовой операции в системе
    /// Содержит полную информацию о движении денежных средств между счетами
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Уникальный идентификатор финансовой записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата и время проведения операции
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Наименование счета-источника или счета-получателя
        /// </summary>
        public string? Account { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Валюта операции (RUB, USD, EUR и т.д.)
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// Родительская категория для группировки операций
        /// </summary>
        public string? ParentCategory { get; set; }

        /// <summary>
        /// Подкатегория для детальной классификации
        /// </summary>
        public string? SubCategory { get; set; }

        /// <summary>
        /// Основная категория операции (еда, транспорт, зарплата и т.д.)
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Контрагент операции (магазин, банк, организация)
        /// </summary>
        public string? Counterparty { get; set; }

        /// <summary>
        /// Счет для переводов между счетами
        /// </summary>
        public string? TransferAccount { get; set; }

        /// <summary>
        /// Сумма перевода между счетами (для операций трансфера)
        /// </summary>
        public decimal? TransferAmount { get; set; }

        /// <summary>
        /// Валюта перевода между счетами
        /// </summary>
        public string? TransferCurrency { get; set; }

        /// <summary>
        /// Теги для дополнительной маркировки и фильтрации операций
        /// </summary>
        public string? Tags { get; set; }

        /// <summary>
        /// Место совершения операции (город, магазин, онлайн и т.д.)
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Дополнительные заметки и комментарии к операции
        /// </summary>
        public string? Note { get; set; }
    }
}
