using System;
using System.Linq;
using MessageService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers
{
    /// <summary>
    /// Контроллер отвечающий за функционал связанный с моделью пользователей.
    /// </summary>
    [Controller]
    [Route("[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// Обработчик HttpPost для регистрации нового пользователя в системе.
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpPost]
        [Route("/[controller]/register-a-new-user/{email:required}/{username:required}")]
        public IActionResult AddNewUser(string email, string username)
        {
            if (ServiceController.Users.Count == 0)
                return BadRequest("Error... Users list have to be initialized before adding new users.");
            
            try
            {
                var user = new User(username, email);
                
                if (ServiceController.Users.Exists(u => u.Email == email))
                    throw new ArgumentException("Error... " +
                                                "There is no option to register user with email of existing user.");
                
                ServiceController.Users.Add(user);
                MethodsForServiceController.UpdateUsersDatabase(ServiceController.SortedUsers);
                
                return Ok(user);
            }
            catch (EmailException)
            {
                return BadRequest("Error... Entered incorrect email address.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Обработчик HttpGet для получения пользователя с введенным адресом e-mail.
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpGet]
        [Route("/[controller]/get-user/{email:required}")]
        public IActionResult GetUserByMail(string email)
        {
            User user = ServiceController.Users.FirstOrDefault(u => u.Email == email);
            
            if (user == null)
                return NotFound($"Error... There is no user with e-mail: {email}.");
            
            return Ok(user);
        }
        
        /// <summary>
        /// Обработчик HttpGet для получения всех пользователей.
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpGet]
        [Route("/[controller]/get-users")]
        public IActionResult GetUsers()
        {
            if (ServiceController.Users.Count == 0)
                return NotFound($"Error... List of users is empty.");
            
            return Ok(ServiceController.SortedUsers);
        }
        
        /// <summary>
        /// Обработчик HttpGet для получения среза списка пользователей (диапазона).
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpGet]
        [Route("/[controller]/get-users/{offset:int:required}/{limit:int:required}")]
        public IActionResult GetUsersWithRange(int offset, int limit)
        {
            if (limit < 1 || offset < 0)
                return BadRequest("Error... Offset have to be greater than -1 and limit have to be greater than 0.");
            
            if (ServiceController.Users.Count <= offset)
                return NotFound("Error... Entered offset value greater than users count.");

            return Ok(limit + offset >= ServiceController.Users.Count ? ServiceController.SortedUsers.ToArray()[offset..] 
                : ServiceController.SortedUsers.ToArray()[offset..(offset + limit)]);
        }
    }
}