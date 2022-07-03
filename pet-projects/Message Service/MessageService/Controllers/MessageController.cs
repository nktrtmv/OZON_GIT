using System.Linq;
using MessageService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers
{
    /// <summary>
    /// Контроллер отвечающий за функционал связанный с моделью сообщений.
    /// </summary>
    [Controller]
    [Route("[controller]")]
    public class MessageController : Controller
    {
       /// <summary>
        /// Обработчик HttpPost для оптравки нового сообщения без темы (e-mail).
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpPost]
        [Route("/[controller]/send-message/{senderId:required}/{receiverId:required}/{message:required}")]
        public IActionResult SendMessage(string senderId,string receiverId, string message)
        {
            if (ServiceController.Messages.Count == 0)
                return BadRequest("Error... Messages list have to be initialized before sending new messages.");
            
            if (!ServiceController.Users.Exists(u => u.Email == receiverId))
                return NotFound($"Error... There is no user with mail: {receiverId}");
            
            if (!ServiceController.Users.Exists(u => u.Email == senderId))
                return NotFound($"Error... There is no user with mail: {senderId}");

            EmailMessage messageToSend = new (message, senderId, receiverId);
            ServiceController.Messages.Add(messageToSend);
            MethodsForServiceController.UpdateMessagesDatabase(ServiceController.Messages);

            return Ok(messageToSend);
        }
        
        /// <summary>
        /// Обработчик HttpPost для оптравки нового сообщения (e-mail).
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpPost]
        [Route("/[controller]/send-message/{senderId:required}/{receiverId:required}/{subject:required}/{message:required}")]
        public IActionResult SendMessage(string senderId,string receiverId,string subject, string message)
        {
            if (ServiceController.Messages.Count == 0)
                return BadRequest("Error... Messages list have to be initialized before sending new messages.");
            
            if (!ServiceController.Users.Exists(u => u.Email == receiverId))
                return NotFound($"Error... There is no user with mail: {receiverId}");
            
            if (!ServiceController.Users.Exists(u => u.Email == senderId))
                return NotFound($"Error... There is no user with mail: {senderId}");

            EmailMessage messageToSend = new (subject, message, senderId, receiverId);
            ServiceController.Messages.Add(messageToSend);
            MethodsForServiceController.UpdateMessagesDatabase(ServiceController.Messages);

            return Ok(messageToSend);
        }
        
        /// <summary>
        /// Обработчик HttpGet для получения всех сообщений отправленных с определенного адреса на определенный адрес (e-mail).
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpGet]
        [Route("/[controller]/get-messages/{senderId:required}/{receiverId:required}")]
        public IActionResult GetMessagesBySenderAndReceiver(string receiverId, string senderId)
        {
            if (!ServiceController.Users.Exists(u => u.Email == receiverId))
                return NotFound($"Error... There is no user with mail: {receiverId}.");
            
            if (!ServiceController.Users.Exists(u => u.Email == senderId))
                return NotFound($"Error... There is no user with mail: {senderId}.");
            
            if (!ServiceController.Messages.Exists(m => m.SenderId == senderId && m.ReceiverId == receiverId))
                return NotFound($"Error... There is no messages with entered sender and receiver mails.");

            return Ok(ServiceController.Messages.Where(m => m.SenderId == senderId && m.ReceiverId == receiverId));
        }
        
        /// <summary>
        /// Обработчик HttpGet для получения всех сообщений отправленных с определенного адреса (e-mail).
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpGet]
        [Route("/[controller]/get-sender-messages/{senderId:required}")]
        public IActionResult GetMessagesBySender(string senderId)
        {
            if (!ServiceController.Users.Exists(u => u.Email == senderId))
                return NotFound($"Error... There is no user with mail: {senderId}.");
            
            if (!ServiceController.Messages.Exists(m => m.SenderId == senderId))
                return NotFound($"Error... There is no messages from user with entered mail.");

            return Ok(ServiceController.Messages.Where(m => m.SenderId == senderId));
        }
        
        /// <summary>
        /// Обработчик HttpGet для получения всех сообщений отправленных на определенный адрес (e-mail).
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpGet]
        [Route("/[controller]/get-receiver-messages/{receiverId:required}")]
        public IActionResult GetMessagesByReceiver(string receiverId)
        {
            if (!ServiceController.Users.Exists(u => u.Email == receiverId))
                return NotFound($"Error... There is no user with mail: {receiverId}.");
            
            if (!ServiceController.Messages.Exists(m => m.SenderId == receiverId))
                return NotFound($"Error... There is no messages to user with entered mail.");

            return Ok(ServiceController.Messages.Where(m => m.ReceiverId == receiverId));
        }

        /// <summary>
        /// Обработчик HttpGet для получения всех сообщений (e-mail).
        /// </summary>
        /// <returns>Контракт результата - ответ на запрос со статусным кодом.</returns>
        [HttpGet]
        [Route("/[controller]/get-all-messages")]
        public IActionResult GetAllMessages()
        {
            if (ServiceController.Messages.Count == 0)
                return NotFound("Error... Messages list is empty.");
            
            return Ok(ServiceController.Messages);
        }
    }
}