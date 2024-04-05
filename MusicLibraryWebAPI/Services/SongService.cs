using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.DataTransferObjects;
using MusicLibraryWebAPI.Models;
using MusicLibraryWebAPI.RabbitMQ;
using System.ComponentModel;

namespace MusicLibraryWebAPI.Services
{
    public class SongService : ISongService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMessageProducer _messageProducer;

        public SongService(ApplicationDbContext context, IMessageProducer messageProducer)
        {
            _context = context;
            _messageProducer = messageProducer;
        }

        public List<Song> GetAllSongs()
        {
            var songs = _context.Songs.ToList();
            return songs;
        }

        public Song GetSong(int id)
        {
            var song = _context.Songs.Find(id);
            return song;
        }



        public async Task SaveSong(SongDto songDto)
        {
             await _messageProducer.SendMessage(songDto);
        }

    }
}
