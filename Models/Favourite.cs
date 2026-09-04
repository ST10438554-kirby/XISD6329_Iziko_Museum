using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IzikoMuseumWebsite.Models
{
    public class Favourite
    {
        [Key]
        public int FavouriteId { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        public int ArtworkId { get; set; }

        [ForeignKey(nameof(ArtworkId))]
        public Artwork? Artwork { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}