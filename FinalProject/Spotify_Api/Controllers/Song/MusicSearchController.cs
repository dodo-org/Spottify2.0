using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotify_Api.DB_Connection;
using Spotify_Api.DB_Connection.Entitys;
using Spotify_Api.Models.Reply;

namespace Spotify_Api.Controllers.Song
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MusicSearchController : ControllerBase
    {
        private readonly BaseContext _context = new BaseContext();

        // API Method for searching
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TitleEntity>>> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            var searchQuery = query.ToLower();

            var results = await _context.Title
                .Include(t => t.Artist)
                .Where(t => t.Name.ToLower().Contains(searchQuery) || t.Artist.StageName.ToLower().Contains(searchQuery))
                .ToListAsync();

            if (!results.Any())
            {
                return NotFound("No matching titles or artists found.");
            }

            List<TitleSearchReply_Model> reply = new List<TitleSearchReply_Model>();
            
            foreach (var result in results)
            {
                reply.Add(new TitleSearchReply_Model(result));
            }
            return Ok(reply);
        }
    }
}
