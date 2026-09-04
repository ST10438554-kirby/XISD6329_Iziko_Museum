using System.ComponentModel.DataAnnotations;

namespace IzikoMuseumWebsite.Models
{
    public class Video
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public string VideoUrl { get; set; } = "";

        public string ThumbnailUrl { get; set; } = "";
    }
}