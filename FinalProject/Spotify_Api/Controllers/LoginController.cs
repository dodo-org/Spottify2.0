using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Spotify_Api.Models.Request;

namespace Spotify_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost(Name ="PostLogin")]
        public string Login([FromBody] LoginRequest_Model Request)
        {
            if(Request.UserName != null && Request.Password != null)
            {
                if(Request.UserName == "dodo")
                {
                    return "Token = token1";
                }
                else
                {
                    return "die logindaten sind Falsch";
                }
            }
            else
            {
                return "Bitte geben sie Beide werte an!";
            }
           return "";
        }

        //[HttpPost(Name = "PostRegister")]
        //public class Register([FromBody] RegisterRequest_Model Request)
        //{
        //    if(Request.UserName != null && Request.Password != null)
        //    {
        //        return "User wurde erstellt";
        //    }
        //    else
        //    {
        //        return "Bitte geben sie Beide werte an!";
        //    }
        //}



    }
}
