using System.ComponentModel.DataAnnotations;

namespace MusicLibraryWebAPI.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
    }
}
