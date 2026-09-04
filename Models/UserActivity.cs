using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IzikoMuseumWebsite.Models
{
    public class UserActivity
    {
        [Key]
        public int ActivityId { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [StringLength(100)]
        public string ActivityType { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime ActivityDate { get; set; } = DateTime.Now;
    }
}