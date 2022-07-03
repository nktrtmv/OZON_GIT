using System;

namespace MessageService.Models
{
    /// <summary>
    /// Класс описывающий сообщение e-mail.
    /// </summary>
    [Serializable]
    public class EmailMessage
    {
        /// <summary>
        /// Паттерн адреса e-mail.
        /// </summary>
        public const string Pattern =
            @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        
        /// <summary>
        /// Тема письма.
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// Текст письма.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// E-mail отправителя.
        /// </summary>
        public string SenderId { get; set; }
        
        /// <summary>
        /// E-mail получателя.
        /// </summary>
        public string ReceiverId { get; set; }

        /// <summary>
        /// Беспараметрический конструктор.
        /// </summary>
        public EmailMessage() { }

        /// <summary>
        /// Конструктор инициализирующий все свойсвта.
        /// </summary>
        /// <param name="subject">Тема письма.</param>
        /// <param name="message">Текст письма.</param>
        /// <param name="senderId">Почта отправителя.</param>
        /// <param name="receiverId">Почта получателя.</param>
        public EmailMessage(string subject, string message, string senderId, string receiverId)
        {
            Subject = subject;
            Message = message;
            SenderId = senderId;
            ReceiverId = receiverId;
        }
        
        /// <summary>
        /// Конструктор инициализирующий все свойсвта кроме темы письма.
        /// </summary>
        /// <param name="message">Текст письма.</param>
        /// <param name="senderId">Почта отправителя.</param>
        /// <param name="receiverId">Почта получателя.</param>
        public EmailMessage(string message, string senderId, string receiverId)
        {
            Message = message;
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}