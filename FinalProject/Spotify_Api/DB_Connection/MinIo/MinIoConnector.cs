using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel;
using Minio.DataModel.Args;
using System.Reactive.Linq;
using Minio.DataModel.Args;
using Spotify_Api.Models.Singeltons;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Spotify_Api.DB_Connection.MinIo
{
    public class MinIoConnector
    {

        private static readonly Lazy<MinIoConnector> _instance = new Lazy<MinIoConnector>(() => new MinIoConnector());

        public static MinIoConnector Instance => _instance.Value;


        public readonly MinioClient _minioClient;
        //public static string minioEndpoint = "minio:9000";
        public static string minioEndpoint = "localhost:9000";
        public static string minioAccessKey = "minio";
        public static string minioSecretKey = "minio123";
        public static string bucketName = "testbucket";
        
        public string TestfileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\MusikTestData";

        public MinIoConnector()
        {
            _minioClient = (MinioClient?)new MinioClient()
            .WithEndpoint(minioEndpoint)
            .WithCredentials(minioAccessKey, minioSecretKey)
            .Build();
            if(_minioClient == null)
            {
                throw new System.Exception("MinIoClient could not be created");
            }
        }

        public async Task<List<string>> GetAllFiles()
        {
            ListObjectsArgs args = new ListObjectsArgs()
                                 .WithBucket(bucketName)
                                 .WithRecursive(true);
            IAsyncEnumerable<Item> observable = _minioClient.ListObjectsEnumAsync(args);
            List<string> res = new List<string>();

            await foreach (var item in observable)
            {
                res.Add(item.Key);
            }

            return res;
        }


        public async Task DeleteAllFilesInBucketAsync()
        {
            try
            {
                ListObjectsArgs args = new ListObjectsArgs()
                                  .WithBucket(bucketName)
                                  .WithRecursive(true);
                IAsyncEnumerable<Item> observable = _minioClient.ListObjectsEnumAsync(args);

                await foreach (var item in observable)
                {
                    await _minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(bucketName).WithObject(item.Key));
                    Console.WriteLine($"Gelöscht: {item.Key}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Löschen der Dateien: {ex.Message}");
            }
        }

        public async Task UploadTestMp3FilesAsync()
        {
            try
            {
                // Stelle sicher, dass das Bucket existiert
                bool bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
                if (!bucketExists)
                {
                    await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                }

                // Lösche alle vorhandenen Dateien im Bucket
                await DeleteAllFilesInBucketAsync();

                // Lade alle MP3-Dateien im angegebenen Ordner hoch
                var files = Directory.GetFiles(TestfileLocation, "*.mp3");

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    await _minioClient.PutObjectAsync(new PutObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(fileName)
                        .WithFileName(file)
                        .WithContentType("audio/mpeg"));

                    Console.WriteLine($"Hochgeladen: {fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }

    }
}
