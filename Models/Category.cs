using System.ComponentModel.DataAnnotations;

namespace IzikoMuseumWebsite.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<Artwork> Artworks { get; set; }
            = new List<Artwork>();
    }
}