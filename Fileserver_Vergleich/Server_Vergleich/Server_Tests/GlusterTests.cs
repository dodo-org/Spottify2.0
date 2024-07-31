using Server_Vergleich.DataStructs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Vergleich.Server_Tests
{
    public class GlusterTests
    {
        private List<string> Songs = new();
        private Times times = new Times();
        private string[] getList = new string[0];

        private static string glusterMountPoint = "C:/mnt/gluster";
        private static string localFilePath = "C:\\Custom\\Projekte\\SystemArchitecture\\Spottify2.0\\Fileserver_Vergleich\\Downloads";

        public Times RunTest(List<string> _Songs, string[] _TitleList, Times _times)
        {
            Songs = _Songs;
            getList = _TitleList;
            times = _times;

            try
            {
                Console.WriteLine(Songs.Count);
                // Songs speichern
                times.StartWrite = DateTime.Now;
                foreach (var file in Songs)
                {
                    Console.WriteLine("Start");
                    var destination = Path.Combine(glusterMountPoint, Path.GetFileName(file));
                    var stopwatch = Stopwatch.StartNew();
                    File.Copy(file, destination);
                    stopwatch.Stop();
                    Console.WriteLine($"GlusterFS Upload: {file} - {stopwatch.ElapsedMilliseconds} ms");
                }

                times.EndWrite = DateTime.Now;
                // Songs auslesen
                times.StartRead = DateTime.Now;
                for (int counter1 = 0; counter1 < getList.Count(); counter1++)
                {
                    var source = Path.Combine(glusterMountPoint, Path.GetFileName(getList[counter1]));
                    var destination = Path.Combine(localFilePath, Path.GetFileName(getList[counter1]));
                    Directory.CreateDirectory(Path.GetDirectoryName(destination));
                    var stopwatch = Stopwatch.StartNew();
                    File.Copy(source, destination);
                    stopwatch.Stop();
                    Console.WriteLine($"GlusterFS Download: {getList[counter1]} - {stopwatch.ElapsedMilliseconds} ms");
                    

                }
                times.EndRead = DateTime.Now;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new();
            }


            times.diffRead = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;

            return times;
        }
    }
}
