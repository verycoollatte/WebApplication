using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{

    /// <summary>
    /// Контроллер сообщений.
    /// </summary>
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
       
        /// <summary>
        /// GET-обработчик (все сообщения).
        /// </summary>
        /// <returns> JSON всех сообщений. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<TextMessage> messages = GenerateController.GetMessages();
            if (messages.Count == 0 || messages.Contains(null))
                return NotFound("Сообщения не найдены.");
            else
                return Ok(messages);
        }

        /// <summary>
        /// GET-обработчик (все сообщения, получатель и отправитель).
        /// </summary>
        /// <param name="sender"> E-mail отправителя. </param>
        /// <param name="receiver"> E-mail получателя. </param>
        /// <returns> JSON сообщений с заданными отправителем и получателем. </returns>
        [HttpGet("{sender}/{receiver}")]
        public IActionResult Get(string sender, string receiver)
        {
            List<TextMessage> theMessages = (List<TextMessage>)GenerateController.GetMessages().Where(e => e.SenderId == sender && e.ReceiverId == receiver).DefaultIfEmpty();

            if (theMessages == null || theMessages.ToList().Count == 0 || theMessages.ToList().Contains(null))
            {
                return NotFound("Сообщения с таким отправителем и таким получателем не найдены.");
            }
            else
            {
                Console.WriteLine(theMessages);
            }

            return Ok(theMessages);

        }


        /// <summary>
        /// GET-обработчик (все сообщения, отправитель).
        /// </summary>
        /// <param name="email"> E-mail отправителя. </param>
        /// <returns> JSON сообщений, отправленных заданным пользователем. </returns>
        [HttpGet("GetByEmailSender/{email}")]
        public IActionResult Get(string email)
        {
            var theMessages = GenerateController.GetMessages().Where(e => e.SenderId == email).DefaultIfEmpty();

            if (theMessages == null || theMessages.ToList().Count == 0 || theMessages.ToList().Contains(null))
            {
                return NotFound("Сообщения с таким отправителем не найдены.");
            }
            else
            {
                Console.WriteLine(theMessages);
            }

            return Ok(theMessages);

        }

        /// <summary>
        /// GET-обработчик (все сообщения, получатель).
        /// </summary>
        /// <param name="email"> E-mail получателя. </param>
        /// <returns>JSON сообщений, полученных заданным пользователем. </returns>

        [HttpGet("GetByEmailReceiver/{email}")]
        public IActionResult GetAnother(string email)
        {
            var theMessages = GenerateController.GetMessages().Where(e => e.ReceiverId == email).DefaultIfEmpty();

            if (theMessages == null || theMessages.ToList().Count == 0 || theMessages.ToList().Contains(null))
            {
                return NotFound("Сообщения с таким получателем не найдены.");
            }
            else
            {
                Console.WriteLine(theMessages);
            }

            return Ok(theMessages);

        }

        /// <summary>
        /// POST-обработчик, добавляет сообщение.
        /// </summary>
        /// <param name="message"> Сообщение. </param>
        /// <returns> Список сообщений. </returns>
        [HttpPost("AddMesssage")]
        public IActionResult PostBody([FromBody] TextMessage message)
        {
            List<User> myUsers = GenerateController.GetUsers();
            List<TextMessage> textMessages = GenerateController.GetMessages();
            User sender = myUsers.SingleOrDefault(p => p.Email == message.SenderId);
            User receiver = myUsers.SingleOrDefault(p => p.Email == message.ReceiverId);
            if (sender == null || receiver == null)
                return NotFound("Отправитель или получатель не найдены.");
            else
                textMessages.Add(message);
            GenerateController.SetMessages(textMessages);
            return Ok(GenerateController.GetMessages());
        }


    }

}
