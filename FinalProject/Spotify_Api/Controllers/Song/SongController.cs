using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Spotify_Api.DB_Connection.MinIo;
using StackExchange.Redis;

namespace Spotify_Api.Controllers.Song
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {


        private readonly IDatabase _redisDb;

        public SongController()
        {
            // Redis Setup
            string redisConnectionString = "localhost:6379";
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            _redisDb = redis.GetDatabase();
        }


        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetMp3(string fileName)
        {
            fileName = fileName + ".mp3";
            try
            {
                string cacheKey = $"audio:{fileName}";

                // Check if the file is already in Redis
                byte[] cachedMp3 = await _redisDb.StringGetAsync(cacheKey);
                if (cachedMp3 != null)
                {
                    return File(new MemoryStream(cachedMp3), "audio/mpeg", fileName);
                }

                

                // Create a memory stream to hold the MP3 data
                using (var memoryStream = new MemoryStream())
                {
                    string bucketname = MinIoConnector.bucketName;
                    // Build the request arguments
                    var getObjectArgs = new GetObjectArgs()
                        .WithBucket(bucketname)
                        .WithObject(fileName)
                        .WithCallbackStream( (stream) =>
                        {
                             stream.CopyTo(memoryStream);
                        });

                    // Get the file from MinIO
                    await MinIoConnector.Instance._minioClient.GetObjectAsync(getObjectArgs);

                    // Set the position back to the beginning of the stream
                    memoryStream.Position = 0;

                    // Store the MP3 file in Redis with an expiration time (e.g., 24 hours)
                    await _redisDb.StringSetAsync(cacheKey, memoryStream.ToArray(), TimeSpan.FromHours(24));
                    memoryStream.Position = 0;
                    // Return the MP3 file as a FileStreamResult
                    memoryStream.Position = 0;
                    return File(memoryStream, "audio/mpeg", fileName);
                }
            }
            catch (MinioException ex)
            {
                throw ex;
                //return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }
    }
}
