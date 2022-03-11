using LoginJwToken.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginJwToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            TokenClass tokenClass = new TokenClass();

            User userObj = new UserRepository().GetUser(user.UserName);
            if (userObj == null)
            {
                tokenClass.TokenOrMessage = "Usuario No Autorizado";
                return Ok(tokenClass);
            }

            bool credentials = userObj.Password.Equals(user.Password);
            if (!credentials)
            {
                tokenClass.TokenOrMessage = "Passoword Incorrecto";
                return Ok(tokenClass);
            }

            tokenClass.TokenOrMessage = TokenManager.GenerateToken(user.UserName);
            return Ok(tokenClass);
        }
    }
}
