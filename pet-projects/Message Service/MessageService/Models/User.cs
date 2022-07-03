using System;
using System.Text.RegularExpressions;

namespace MessageService.Models
{
    /// <summary>
    /// Класс описывающий пользователя.
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Адрес почты.
        /// </summary>
        /// <exception cref="EmailException">Выбрасывается при присваивании некорректного адреса почты.</exception>
        public string Email
        {
            get => _email;
            set
            {
                if (Regex.IsMatch(value, EmailMessage.Pattern, RegexOptions.IgnoreCase))
                    _email = value;
                else
                    throw new EmailException("Incorrect email!");
            }
        }

        private string _email;

        /// <summary>
        /// Беспараметрический конструктор.
        /// </summary>
        public User() { }

        /// <summary>
        /// Инициализирует объект класса с заданными свойствами.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="email">Почта пользователя.</param>
        public User(string username, string email)
        {
            UserName = username;
            Email = email;
        }
    }
}