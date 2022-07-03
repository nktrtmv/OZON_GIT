using System;
using System.Runtime.Serialization;

namespace MessageService.Models
{
    /// <summary>
    /// Exception выбрасываемый при некорректном адресе e-mail.
    /// </summary>
    [Serializable]
    public class EmailException : Exception
    {
        /// <summary>
        /// Беспараметрический конструктор.
        /// </summary>
        public EmailException() { }

        /// <summary>
        /// Инициаилизирует сериализуемый объект класса.
        /// </summary>
        /// <param name="info">Объект для сериализации.</param>
        /// <param name="context">Контекстная информация.</param>
        protected EmailException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Инициализирует объект класса с сообщением об ошибке.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public EmailException(string message) : base(message) { }

        /// <summary>
        /// Инициализирует объект класса с сообщением об ошибке и информацией об inner exception.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="innerException">Информация об inner exception.</param>
        public EmailException(string message, Exception innerException) : base(message, innerException) { }
    }
}