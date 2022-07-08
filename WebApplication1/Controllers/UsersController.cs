using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Контроллер пользователей.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// GET-обработчик (все пользователи).
        /// </summary>
        /// <returns> JSON пользователей. </returns>
        [HttpGet("{limit?}/{offset?}")]
        public IActionResult Get(int limit = 100, int offset = 0) { 
            List<User> users = GenerateController.GetUsers();
            if (offset < 0 || limit <= 0)
                return BadRequest("Ой...");
            if (offset > users.Count)
                return NotFound("Пользователи не найдены.");
            List<User> myUsers = new();
            for (var i = offset; i < users.Count; i++) 
            {
                
                if (myUsers.Count >= limit)
                    break;
                myUsers.Add(users[i]);
            }
            if (myUsers.Count == 0 || myUsers.Contains(null))
                return NotFound("Пользователи не найдены.");
            else
                return Ok(myUsers);
        }

        /// <summary>
        /// GET-обработчик (пользователь по e-mail).
        /// </summary>
        /// <param name="email"> Email пользователя. </param>
        /// <returns> Пользователь с заданным e-mailом. </returns>
        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            User user = GenerateController.GetUsers().SingleOrDefault(p => p.Email == email);

            if (user == null)
                return NotFound("Такой пользователь не найден.");
            return Ok(user);
        }

        /// <summary>
        /// POST-обработчик, добавляет пользователя.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        /// <returns> Список пользователей. </returns>
        [HttpPost("AddUser")]
        public IActionResult PostBody([FromBody] User user)
        {
            List<User> myUsers = GenerateController.GetUsers();
            User sameUser = myUsers.SingleOrDefault(p => p.Email == user.Email);
            if (!user.Email.Contains("@"))
                return BadRequest("Почта без собачки -- не почта. Грр.");
            if (sameUser != null)
                sameUser.UserName = user.UserName;
            else
                myUsers.Add(user);
            GenerateController.SetUsers(myUsers);
            return Ok(GenerateController.GetUsers());

        }

    }

}
