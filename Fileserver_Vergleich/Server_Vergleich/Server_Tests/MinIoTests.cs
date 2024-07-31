using Minio;
using Minio.DataModel.Args;
using Server_Vergleich.DataStructs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server_Vergleich.Server_Tests
{
    public class MinIoTests
    {
        private List<string> Songs = new();
        private Times times = new Times();
        private string[] getList = new string[0];

        private static string minioEndpoint = "localhost:9000";
        private static string minioAccessKey = "minio";
        private static string minioSecretKey = "minio123";
        private static string bucketName = "testbucket";
        private static string localFilePath = "C:\\Custom\\Projekte\\SystemArchitecture\\Spottify2.0\\Fileserver_Vergleich\\Downloads";

        public async Task<Times> RunTest(List<string> _Songs, string[] _TitleList, Times _times)
        {
            Songs = _Songs;
            getList = _TitleList;
            times = _times;



            try
            {

                var minio = new MinioClient()
                .WithEndpoint(minioEndpoint)
                .WithCredentials(minioAccessKey, minioSecretKey)
                .Build();

                bool found = await minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
                if (!found)
                {
                    await minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                }

                

            



                // Verbindung zur MongoDB aufbauen (lokale MongoDB auf Standardport)


            // Datenbank und Kollektion auswählen

            // Songs speichern
                times.StartWrite = DateTime.Now;
                foreach (string song in Songs)
                {

                    var stopwatch = Stopwatch.StartNew();
                    await minio.PutObjectAsync(new PutObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(Path.GetFileName(song))
                        .WithFileName(song)
                        .WithContentType("application/octet-stream"));
                    stopwatch.Stop();
                    Console.WriteLine($"MinIO Upload: {song} - {stopwatch.ElapsedMilliseconds} ms");
                }
                times.EndWrite = DateTime.Now;
                // Songs auslesen
                times.StartRead = DateTime.Now;
                for (int counter1 = 0; counter1 < getList.Count(); counter1++)
                {
                    var stopwatch = Stopwatch.StartNew();
                    await minio.GetObjectAsync(new GetObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(Path.GetFileName(getList[counter1]))
                        .WithCallbackStream(stream =>
                        {
                            
                            using (var fileStream = File.Create(Path.Combine(localFilePath, Path.GetFileName(getList[counter1]))))
                            {
                                stream.CopyTo(fileStream);
                            }
                        }));
                    stopwatch.Stop();
                    Console.WriteLine($"MinIO Download: {getList[counter1]} - {stopwatch.ElapsedMilliseconds} ms");
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
