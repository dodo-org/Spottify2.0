using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Vergleich.DataStructs
{
    public struct Times
    {
        public DateTime StartWrite { get; set; }
        public DateTime StartRead { get; set; }
        public DateTime EndWrite { get; set; }
        public DateTime EndRead { get; set; }
        public TimeSpan diffWrite { get; set; }
        public TimeSpan diffRead { get; set; }
        public Times()
        {
            StartWrite = new DateTime();
            StartRead = new DateTime();
            EndWrite = new DateTime();
            EndRead = new DateTime();
            diffRead = new TimeSpan(0);
            diffWrite = new TimeSpan(0);
        }

        public Times(
            DateTime i_StartWrite,
            DateTime i_StartRead,
            DateTime i_EndWrite,
            DateTime i_EndRead
            )
        {
            StartWrite = i_StartWrite;
            StartRead = i_StartRead;
            EndWrite = i_EndWrite;
            EndRead = i_EndRead;
            diffRead = new TimeSpan(0);
            diffWrite = new TimeSpan(0);
        }
    }
}
