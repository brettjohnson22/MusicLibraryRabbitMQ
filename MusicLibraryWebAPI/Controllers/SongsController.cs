using Microsoft.AspNetCore.Mvc;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.DataTransferObjects;
using MusicLibraryWebAPI.Models;
using MusicLibraryWebAPI.RabbitMQ;
using MusicLibraryWebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISongService _songService;

        public SongsController(ApplicationDbContext context, ISongService songService)
        {
            _context = context;
            _songService = songService;
        }

        // GET: api/Songs
        [HttpGet]
        public IActionResult Get()
        {
            var Songs = _context.Songs.ToList();
            return Ok(Songs);
        }

        // GET api/Songs/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Song = _context.Songs.Find(id);
            return Ok(Song);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SongDto songDto)
        {
            try
            {
                await _songService.SaveSong(songDto);
                return Accepted(songDto); 
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // PUT api/Songs/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {
            var songFromDb = _context.Songs.Find(id);

            if (songFromDb == null)
            {
                return NotFound(); // Return 404 Not Found if the song with the specified ID is not found
            }

            // Update the properties of songFromDb with the values from song
            songFromDb.Title = song.Title;
            songFromDb.Artist = song.Artist;

            _context.SaveChanges();

            return Ok(songFromDb);
        }

        // DELETE api/Songs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var SongFromDb = _context.Songs.Find(id);
            _context.Songs.Remove(SongFromDb);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
