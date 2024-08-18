using Spottify2.Models.Request;

namespace Spottify2.Models.Request
{
    public class LoginRequest_Model : BaseRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
