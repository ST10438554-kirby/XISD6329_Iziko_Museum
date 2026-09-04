using System.ComponentModel.DataAnnotations;

namespace IzikoMuseumWebsite.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Role { get; set; } = "Visitor";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // User's favourite artworks
        public ICollection<Favourite> Favourites { get; set; }
            = new List<Favourite>();

        // User activity records
        public ICollection<UserActivity> Activities { get; set; }
            = new List<UserActivity>();
    }
}