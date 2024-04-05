using MusicLibraryWebAPI.DataTransferObjects;
using MusicLibraryWebAPI.Models;

namespace MusicLibraryWebAPI.Services
{
    public interface ISongService
    {

        Task SaveSong(SongDto songDto);
    }
}
