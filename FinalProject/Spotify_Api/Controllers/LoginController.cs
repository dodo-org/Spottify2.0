using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Spotify_Api.DB_Connection;
using Spotify_Api.DB_Connection.Entitys;
using Spotify_Api.Models.Request;

namespace Spotify_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region Constructor

        LoginController() 
        { 
            _dbContext = new BaseContext();
        }




        #endregion

        private BaseContext _dbContext;



        [HttpPost(Name ="PostLogin")]
        public IActionResult Login([FromBody] LoginRequest_Model Request)
        {
            if(Request.UserName != null && Request.Password != null)
            {
                if(Request.UserName == "dodo")
                {
                    return Ok("Token = token1");
                }
                else
                {
                    return Unauthorized("die logindaten sind Falsch");
                }
            }
            else
            {
                return Unauthorized("Bitte geben sie Beide werte an!");
            }
        }





    }
}
