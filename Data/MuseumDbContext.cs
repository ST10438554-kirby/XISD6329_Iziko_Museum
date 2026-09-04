using Microsoft.EntityFrameworkCore;
using IzikoMuseumWebsite.Models;

namespace IzikoMuseumWebsite.Data
{
    public class MuseumDbContext : DbContext
    {
        public MuseumDbContext(
            DbContextOptions<MuseumDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artwork> Artworks { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Favourite> Favourites { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<QRCode> QRCodes { get; set; }
    }
}