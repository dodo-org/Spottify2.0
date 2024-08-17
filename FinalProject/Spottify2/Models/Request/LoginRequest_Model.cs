using Spottify2.Models.Request;

namespace Spotify_Api.Models.Request
{
    public class LoginRequest_Model : BaseRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
