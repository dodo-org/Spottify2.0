using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Spotify_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost(Name ="PostLogin")]
        public string Login([FromBody] string UserName, [FromBody] string Password)
        {
            if(UserName != null && Password != null)
            {
                if(UserName == "dodo")
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

    }
}
