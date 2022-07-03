using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MessageService.Controllers;
using MessageService.Models;

namespace MessageService
{
    public static class MethodsForServiceController
    {
        /// <summary>
        /// Генерация пользователей с помощью <see cref="Random"/>.
        /// </summary>
        /// <param name="random">Объект <see cref="Random"/>.</param>
        /// <param name="users">Список пользователей.</param>
        private static void GenerateUser(Random random, ICollection<User> users)
        {
            StringBuilder nameBuilder = new();
            StringBuilder mailBuilder = new();
            int mailLength = random.Next(5, 11);
            int nameLength = random.Next(5, 9);
            
            for (var i = 0; i < mailLength; i++)
            {
                mailBuilder.Append(random.Next(1, 5) == 4 && mailBuilder.Length > 5 ? (char)random.Next(48, 58) : (char)random.Next(97, 123));
            }
            mailBuilder.Append('@');
            switch (random.Next(0,5))
            {
                case 0:
                    mailBuilder.Append("gmail.com");
                    break;
                case 1:
                    mailBuilder.Append("icloud.com");
                    break;
                case 2:
                    mailBuilder.Append("yandex.ru");
                    break;
                case 3:
                    mailBuilder.Append("mail.ru");
                    break;
                case 4:
                    mailBuilder.Append("edu.hse.ru");
                    break;
            }
            
            for (var i = 0; i < nameLength; i++)
            {
                nameBuilder.Append(i == 0 ? (char)random.Next(65, 91) : (char)random.Next(97, 123));
            }

            users.Add(new User(nameBuilder.ToString(), mailBuilder.ToString()));
        }

        private static readonly string[] s_subjects = {"education", "work", "entertainment", "personal", "humoresque"};

        private static readonly string[] s_humoresque =
        {
            "В окно постучали: «Чистка окон»- подумал Штирлиц. «Кар»- легко парировала стукнувшаяся ворона",
            "В дверь постучали очередью. 'Автомат', - подумал Штирлиц. 'АХАХАХАХА', - " +
            "не сдержалась Вышка. Это же перваки в учебке",
            "В дверь постучали, Штирлиц открыл дверь. За дверью стоял человек в фуфайке и в лыжах. 'Фуфлыжник', - подумал Штирлиц",
            "Подвыпившие Штирлиц и Мюллер вышли из бара. - Давайте снимем девочек, - предложил Штирлиц. - " +
            "У вас очень доброе сердце - ответил Мюллер. - Но пусть все-таки повисят до утра.",
            "В дверь постучали 1755 раз. «МГУшник», - смекнул Штирлиц. 'Дурак' - ответило 0,9637 стипендии",
            "В дверь постучали 10 раз, потом 8, затем 6. «Инфляция оценок», – догадался Штирлиц.",
            "Штирлиц долго смотрел в точку. Потом в другую. 'Двоеточие!', - наконец-то смекнул Штирлиц",
            "Штирлиц шел по лесу и видел голубые ели, Штирлиц пригляделся и увидел, что голубые еще и пили",
            "В дверь постучались 4 раза по 4 раза. 'хорошист!', - подумал Штирлиц. 'Иди н***й', легко парировал слетевший" +
            " с 70%-ной скидки студент вышки"
        };
        
        /// <summary>
        /// Получение рандомной ЮМОРЭСКИ!!!
        /// </summary>
        /// <param name="random">Объект <see cref="Random"/>.</param>
        /// <returns>Рандомную юмореску.</returns>
        private static string GetHumoresque(Random random) => s_humoresque[random.Next(0, s_humoresque.Length - 1)];
        
        /// <summary>
        /// Генерация сообщений с помощью обекта Random.
        /// </summary>
        /// <param name="random">Объект <see cref="Random"/>.</param>
        /// <param name="messages">Список сообщений.</param>
        private static void GenerateMessage(Random random, ICollection<EmailMessage> messages)
        {
            string subject = s_subjects[random.Next(0, s_subjects.Length)];
            string message = subject != "humoresque" ? $"Some {subject} message." : GetHumoresque(random);
            string receiverMail = ServiceController.Users[random.Next(0, ServiceController.Users.Count)].Email;
            string senderMail;
            while ((senderMail = ServiceController.Users[random.Next(0, ServiceController.Users.Count)].Email) == receiverMail) {}

            messages.Add(new EmailMessage(subject, message, senderMail, receiverMail));
        }

        /// <summary>
        /// Обновляет базу данных пользователей.
        /// </summary>
        public static void UpdateUsersDatabase(List<User> users)
        {
            try
            {
                if (!Directory.Exists("Database"))
                {
                    Directory.CreateDirectory("Database");
                }

                string jsonString = JsonSerializer.Serialize(users);
                using var sw = new StreamWriter($"Database{Path.DirectorySeparatorChar}Users.json");
                sw.WriteLine(jsonString);
            }
            catch
            {
                //
            }
        }
        
        /// <summary>
        /// Обновляет базу данных сообщений.
        /// </summary>
        public static void UpdateMessagesDatabase(List<EmailMessage> messages)
        {
            try
            {
                if (!Directory.Exists("Database"))
                {
                    Directory.CreateDirectory("Database");
                }

                string jsonString = JsonSerializer.Serialize(messages);
                using var sw = new StreamWriter($"Database{Path.DirectorySeparatorChar}Messages.json");
                sw.WriteLine(jsonString);
            }
            catch
            {
                //
            }
        }
        
        /// <summary>
        /// Рандомно генерирует новых пользователей и сообщения.
        /// </summary>
        public static async void GenerateNewRandomUsersAndMessages(List<User> users, List<EmailMessage> messages)
        {
            try
            {
                Random random = new();
                int usersCount = random.Next(5, 16);
                int messagesCount = random.Next(usersCount, usersCount * 3 + 1);

                for (int i = 0; i < usersCount; i++)
                {
                    GenerateUser(random, users);
                }

                for (int i = 0; i < messagesCount; i++)
                {
                    GenerateMessage(random, messages);
                }

                UpdateMessagesDatabase(messages);
                UpdateUsersDatabase(users);
            }
            catch
            {
                await Task.Delay(3000);
                GenerateNewRandomUsersAndMessages(users,messages);
            }
        }
    }
}