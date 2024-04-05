using MusicLibraryWebAPI.DataTransferObjects;
using MusicLibraryWebAPI.Models;

namespace MusicLibraryWebAPI.RabbitMQ
{
    public class SongMessage
    {
        public string Operation { get; set; }
        public SongDto Payload { get; set; }
    }
}
