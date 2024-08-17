using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotify_Api.DB_Connection;
using Spotify_Api.DB_Connection.Entitys;
using Spotify_Api.Models.Request;

namespace Spotify_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private BaseContext _dbContext = new BaseContext();

        [HttpPost(Name = "PostRegister")]
        public IActionResult Register([FromBody] RegisterRequest_Model Request)
        {
            
            bool exist = false;

            UserEntity Reg_user = new UserEntity()
            {
                UserName = Request.UserName,
                Email = Request.Email,
                Password = Request.Password
            };

            // DB abfrage ob exisitiert oder nicht

            List<UserEntity> existUser = _dbContext.User
                .Where(x => x.UserName == Reg_user.UserName)
                .ToList();
            if (existUser.Count != 0)
            {
                exist = true;
                return Unauthorized("Der UserName existiert bereits");
            }


            existUser = _dbContext.User
                .Where(x => x.Email == Reg_user.Email)
                .ToList();
            if (existUser.Count != 0)
            {
                exist = true;
                return Unauthorized("Die Email existiert bereits");
            }

            if (exist == false)
            {
                //Todo: Password hashen?
                _dbContext.User.Add(Reg_user);
                _dbContext.SaveChanges();

                //Todo: GGF gleich mit Token antworten?
                return Ok("Der User wurde erstellt");
            }
            else
            {
                return Unauthorized("Der User konnte nicht erstellt werden");
            }
            
        }
    }
}
