using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IzikoMuseumWebsite.Models
{
    public class QRCode
    {
        [Key]
        public int QRCodeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Code { get; set; } = string.Empty;

        public int ArtworkId { get; set; }

        [ForeignKey("ArtworkId")]
        public Artwork? Artwork { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}