using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IzikoMuseumWebsite.Models
{
    public class Artwork
    {
        [Key]
        public int ArtworkId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? YearCreated { get; set; }

        public string? Medium { get; set; }


        // =========================
        // ARTIST
        // =========================

        public int? ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public Artist? Artist { get; set; }


        // =========================
        // CATEGORY
        // =========================

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }


        // =========================
        // GALLERY
        // =========================

        public int? GalleryId { get; set; }

        [ForeignKey("GalleryId")]
        public Gallery? Gallery { get; set; }


        // =========================
        // FAVOURITES
        // =========================

        public ICollection<Favourite> Favourites { get; set; }
            = new List<Favourite>();
    }
}