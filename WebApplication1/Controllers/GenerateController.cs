using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Контроллер генераций.
    /// </summary>
    [Route("[controller]")]
    public class GenerateController : ControllerBase
    {
        /// <summary>
        /// POST-обработчик. Рандомное заполнение списков пользователей и писем.
        /// </summary>
        /// <returns> JSON всех пользователей. </returns>
        [HttpPost("start")]
        public IActionResult Post()
        {
            Random random = new Random();
            int count = random.Next(2, 8);
            List<User> newUsers = new();
            for (var i = 0; i < count; i++)
            {
                User randomUser = RandomUser();
                User sameUsers = newUsers.SingleOrDefault(p => p.Email == randomUser.Email);
                while (sameUsers != null)
                {
                    randomUser = RandomUser();
                    sameUsers = newUsers.SingleOrDefault(p => p.Email == randomUser.Email);
                }
                newUsers.Add(randomUser);
            }
            count = random.Next(4, 8);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            List<TextMessage> newMessages = new();
            for (var i = 0; i < count; i++)
            {
                string sender = newUsers[random.Next(0, newUsers.Count)].Email;
                string receiver = newUsers[random.Next(0, newUsers.Count)].Email;
                newMessages.Add(
                    new TextMessage()
                    {
                        Subject = new string(Enumerable.Repeat(chars, random.Next(3, 8))
                 .Select(s => s[random.Next(s.Length)]).ToArray())
                    ,
                        Message = new string(Enumerable.Repeat(chars, random.Next(3, 8))
                 .Select(s => s[random.Next(s.Length)]).ToArray()),
                        ReceiverId = receiver,
                        SenderId = sender
                    });

            }
            SetUsers(newUsers);
            SetMessages(newMessages);
            return Ok(GetUsers());

        }

        /// <summary>
        /// Достает сообщения из файла в список.
        /// </summary>
        /// <returns> Лист сообщений. </returns>
        [NonAction]
        public static List<TextMessage> GetMessages()
        {
            List<TextMessage> messages = new();
            DataContractJsonSerializer jsonSerializer = new(typeof(List<TextMessage>));
            try
            {
                using (FileStream fs = new FileStream("messages.json", FileMode.Open))
                {
                    messages = jsonSerializer.ReadObject(fs) as List<TextMessage>;
                }
            }
            catch
            {
                return messages;
            }
            return messages;
        }

        /// <summary>
        /// Заменяет информацию о сообщениях в файле на новую.
        /// </summary>
        /// <param name="messages"> Лист сообщений. </param>
        [NonAction]
        public static void SetMessages(List<TextMessage> messages)
        {
            DataContractJsonSerializer jsonSerializer = new(typeof(List<TextMessage>));
            try
            {
                using (FileStream fs = new FileStream("messages.json", FileMode.Create))
                {
                    jsonSerializer.WriteObject(fs, messages);
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Достает пользователей из файла в список.
        /// </summary>
        /// <returns> Лист пользователей. </returns>
        [NonAction]
        public static List<User> GetUsers()
        {
            List<User> users = new();
            DataContractJsonSerializer jsonSerializer = new(typeof(List<User>));
            try
            {
                using (FileStream fs = new FileStream("users.json", FileMode.OpenOrCreate))
                {
                    users = jsonSerializer.ReadObject(fs) as List<User>;
                }
            }
            catch
            {
                return users;
            }
            return users;
        }

        /// <summary>
        /// Заменяет информацию о пользователях в файле на новую.
        /// </summary>
        /// <param name="users"> Лист пользователей. </param>
        [NonAction]
        public static void SetUsers(List<User> users)
        {
            try
            {
                users.Sort();
                DataContractJsonSerializer jsonSerializer = new(typeof(List<User>));
                using (FileStream fs = new FileStream("users.json", FileMode.Create))
                {
                    jsonSerializer.WriteObject(fs, users);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Генерирует рандомного пользователя.
        /// </summary>
        /// <returns> Пользователь. </returns>
        [NonAction]
        public User RandomUser()
        {
            Random random = new Random();
            int lengthName = random.Next(3, 7);
            int lengthEmail = random.Next(2, 5);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new User()
            {
                UserName = new string(Enumerable.Repeat(chars, lengthName)
                 .Select(s => s[random.Next(s.Length)]).ToArray()),
                Email = new string(Enumerable.Repeat(chars, lengthEmail)
                 .Select(s => s[random.Next(s.Length)]).ToArray()) + "@hse.ru"
            };
        }
    }
}
