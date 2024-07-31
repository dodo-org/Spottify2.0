using Server_Vergleich.DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Vergleich.Server_Tests
{
    public class NsfTests
    {
        private List<string> users = new();
        private Times times = new Times();
        private string[] getList = new string[0];
        public Times RunTest(List<string> _Songs, string[] _TitleList, Times _times)
        {
            users = _Songs;
            getList = _TitleList;
            times = _times;

            try
            {
                // Verbindung zur MongoDB aufbauen (lokale MongoDB auf Standardport)


                // Datenbank und Kollektion auswählen

                // Songs speichern
                times.StartWrite = DateTime.Now;
                foreach (string user in users)
                {


                }
                times.EndWrite = DateTime.Now;
                // Songs auslesen
                times.StartRead = DateTime.Now;
                for (int counter1 = 0; counter1 < getList.Count(); counter1++)
                {

                }
                times.EndRead = DateTime.Now;
            }
            catch
            {
                return new();
            }


            times.diffRead = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;

            return times;
        }
    }
}
