using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spottify2.Models.Reply
{
    class TitleSearchReply_Model
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int artistID { get; set; }
        public string artist { get; set; }
    }
}
