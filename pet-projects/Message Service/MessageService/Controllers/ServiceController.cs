using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MessageService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers
{
    /// <summary>
    /// Контроллер отвечающий за функционал связанный с моделями сообщений и пользователей.
    /// </summary>
    [Controller]
    [Route("[controller]")]
    public class ServiceController : Controller
    {
        /// <summary>
        /// Инициализирует списки сообщений и пользователей, записывает данные из бд в списки.
        /// </summary>
        static ServiceController()
        {
            Users = new List<User>();
            Messages = new List<EmailMessage>();
        }

        /// <summary>
        /// Пользователи.
        /// </summary>
        public static List<User> Users { get; set; }
        
        /// <summary>
        /// Сортированный список пользователей.
        /// </summary>
        public static List<User> SortedUsers => Users.OrderBy(u => u.Email[..3]).ToList();

        /// <summary>
        /// Сообщения пользователей.
        /// </summary>
        public static List<EmailMessage> Messages { get; set; }

        /// <summary>
        /// Обработчик HttpPost для инициализации списков пользователей и сообщений - рандомная генерация.
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpPost]
        [Route("/[controller]/initialize")]
        public IActionResult InitializeList()
        {
            if (Users.Count > 0)
                return BadRequest("Error... Users list is already initialized.");
            
            (List<User> users, List<EmailMessage> emailMessages) = (Users, Messages);
            try
            {
                string path = $"Database{Path.DirectorySeparatorChar}";
                if (!Directory.Exists("Database"))
                {
                    Users = users;
                    Messages = emailMessages;
                }
                else
                {
                    string jsonString = System.IO.File.ReadAllText(path + "Users.json");
                    Users = JsonSerializer.Deserialize<List<User>>(jsonString);

                    jsonString = System.IO.File.ReadAllText(path + "Messages.json");
                    Messages = JsonSerializer.Deserialize<List<EmailMessage>>(jsonString);
                }
            }
            catch
            {
                Users = users;
                Messages = emailMessages;
            }

            if (Users is null || Messages is null || Users.Count == 0 || Messages.Count == 0)
                MethodsForServiceController.GenerateNewRandomUsersAndMessages(Users,Messages);

            return Ok("Lists initialized!");
        }

        /// <summary>
        /// Добавление новых рандомных пользователей и сообщений.
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpPut]
        [Route("/[controller]/add-new")]
        public IActionResult AddNewRandomData()
        {
            if (Users.Count == 0)
                return BadRequest("Error... Users list have to be initialized before generating new data.");
            
            MethodsForServiceController.GenerateNewRandomUsersAndMessages(Users, Messages);
            
            return Ok("New data added successfully.");
        }

        /// <summary>
        /// Удаление всех данных из файлов и списков.
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpDelete]
        [Route("/[controller]/delete-all")]
        public IActionResult DeleteFilesAndLists()
        {
            Users = new List<User>();
            Messages = new List<EmailMessage>();
            System.IO.File.Delete($"Database{Path.DirectorySeparatorChar}Users.json");
            System.IO.File.Delete($"Database{Path.DirectorySeparatorChar}Messages.json");
            
            return Ok("Data deleted successfully.");
        }
    }
}