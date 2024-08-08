using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Spotify_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTestResponse")]
        public string Get()
        {
            return "hallo es klappt";
        }

        [HttpPost]
        public string Post(string Input1)
        {

            return Input1;
        }

    }
}
