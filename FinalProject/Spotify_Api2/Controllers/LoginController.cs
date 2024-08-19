using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Spotify_Api.DB_Connection;
using Spotify_Api.DB_Connection.Entitys;
using Spotify_Api.Models.Reply;
using Spotify_Api.Models.Request;
using Spotify_Api.Models.Singeltons;

namespace Spotify_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private BaseContext _dbContext= new BaseContext();



        [HttpPost(Name ="PostLogin")]
        public IActionResult Login([FromBody] LoginRequest_Model Request)
        {
            UserEntity? existUser = null;
            //Find User By user Name
            try
            {
                existUser = _dbContext.User
                    .Where(x => x.UserName == Request.UserName)
                    .Single();
            }
            catch
            {
                return Unauthorized("Der User existiert nicht");
            }
            
            //Todo: Hash Password 
            if(existUser.Password == Request.Password)
            {
                // generate Token

                LoginReply_Model Reply = new LoginReply_Model()
                {
                    Token = Token_Helper.Instance.GenerateJwtToken(existUser.UserName)
                };

                // Return Token
                return Ok(Reply);

            }
            else
            {
                return Unauthorized("Das Passwort ist falsch");
            }
            
        }





    }
}
