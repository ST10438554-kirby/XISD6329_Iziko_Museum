using System.ComponentModel.DataAnnotations;

namespace IzikoMuseumWebsite.Models
{
    public class Gallery
    {
        [Key]
        public int GalleryId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }

        public ICollection<Artwork> Artworks { get; set; }
            = new List<Artwork>();
    }
}