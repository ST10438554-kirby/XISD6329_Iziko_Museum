using IzikoMuseumWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace IzikoMuseumWebsite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Artwork> Artworks { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<Favourite> Favourites { get; set; }

        public DbSet<UserActivity> UserActivities { get; set; }

        public DbSet<QRCode> QRCodes { get; set; }


        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // =========================
            // TABLE NAMES
            // =========================

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Artist>()
                .ToTable("Artists");

            modelBuilder.Entity<Artwork>()
                .ToTable("Artworks");

            modelBuilder.Entity<Category>()
                .ToTable("Categories");

            modelBuilder.Entity<Gallery>()
                .ToTable("Galleries");

            modelBuilder.Entity<Favourite>()
                .ToTable("Favourites");

            modelBuilder.Entity<UserActivity>()
                .ToTable("UserActivities");

            modelBuilder.Entity<QRCode>()
                .ToTable("QRCodes");


            // =========================
            // USER
            // =========================

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();


            // =========================
            // ARTIST → ARTWORK
            // =========================

            modelBuilder.Entity<Artwork>()
                .HasOne(a => a.Artist)
                .WithMany(a => a.Artworks)
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.SetNull);


            // =========================
            // CATEGORY → ARTWORK
            // =========================

            modelBuilder.Entity<Artwork>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Artworks)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);


            // =========================
            // GALLERY → ARTWORK
            // =========================

            modelBuilder.Entity<Artwork>()
                .HasOne(a => a.Gallery)
                .WithMany(g => g.Artworks)
                .HasForeignKey(a => a.GalleryId)
                .OnDelete(DeleteBehavior.SetNull);


            // =========================
            // USER → FAVOURITES
            // =========================

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // ARTWORK → FAVOURITES
            // =========================

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.Artwork)
                .WithMany(a => a.Favourites)
                .HasForeignKey(f => f.ArtworkId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // PREVENT DUPLICATE
            // =========================

            modelBuilder.Entity<Favourite>()
                .HasIndex(f => new
                {
                    f.UserId,
                    f.ArtworkId
                })
                .IsUnique();


            // =========================
            // USER → ACTIVITY
            // =========================

            modelBuilder.Entity<UserActivity>()
                .HasOne(a => a.User)
                .WithMany(u => u.Activities)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);


            // =========================
            // QR CODE → ARTWORK
            // =========================

            modelBuilder.Entity<QRCode>()
                .HasOne(q => q.Artwork)
                .WithMany()
                .HasForeignKey(q => q.ArtworkId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<QRCode>()
                .HasIndex(q => q.Code)
                .IsUnique();
        }
    }
}