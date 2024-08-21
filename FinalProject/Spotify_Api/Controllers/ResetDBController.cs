using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotify_Api.DB_Connection.MinIo;
using Spotify_Api.DB_Connection.TestData;

namespace Spotify_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResetDBController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> ResetDB()
        {
            CreateAndAplyTestData createAndAplyTestData = new CreateAndAplyTestData();
            createAndAplyTestData.ApplyTestData();

            
            await MinIoConnector.Instance.DeleteAllFilesInBucketAsync();
            await MinIoConnector.Instance.UploadTestMp3FilesAsync();
            var res = MinIoConnector.Instance.GetAllFiles().Result;

            return Ok(res);
        }
    }
}
