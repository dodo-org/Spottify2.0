using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spottify2.Models.Reply
{
    public class RegistrationReply_Model
    {
        public string token { get; set; }
        public RegistrationResponses Response { get; set; }
    }

    public enum RegistrationResponses
    {
        Success,
        UsernameExists,
        EmailExists
    }
}
