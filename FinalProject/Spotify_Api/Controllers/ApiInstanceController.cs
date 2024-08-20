using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Spotify_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiInstanceController : ControllerBase
    {
        [HttpGet]
        public string GetApiInstance()
        {
            string hostname = Environment.MachineName;
            return $"Instance: {hostname}";
            // return "Instance: 1";
        }
    }
}
