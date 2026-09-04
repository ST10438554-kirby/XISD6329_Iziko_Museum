using System.ComponentModel.DataAnnotations;

namespace IzikoMuseumWebsite.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Biography { get; set; }

        public string? Nationality { get; set; }

        public int? BirthYear { get; set; }

        public int? DeathYear { get; set; }

        public string? ImageUrl { get; set; }

        // Artist's artworks
        public ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();
    }
}