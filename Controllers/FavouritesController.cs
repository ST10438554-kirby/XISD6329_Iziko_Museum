using IzikoMuseumWebsite.Data;
using IzikoMuseumWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzikoMuseumWebsite.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavouritesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // FAVOURITES PAGE
        // =========================

        public async Task<IActionResult> Index()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (!int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var favourites = await _context.Favourites
                .Include(f => f.Artwork)
                .ThenInclude(a => a!.Artist)
                .Include(f => f.Artwork)
                .ThenInclude(a => a!.Category)
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            return View(favourites);
        }

        // =========================
        // ADD FAVOURITE
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int artworkId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (!int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var artwork = await _context.Artworks
                .FirstOrDefaultAsync(a => a.ArtworkId == artworkId);

            if (artwork == null)
            {
                return NotFound();
            }

            var alreadyFavourite = await _context.Favourites
                .AnyAsync(f =>
                    f.UserId == userId &&
                    f.ArtworkId == artworkId);

            if (!alreadyFavourite)
            {
                var favourite = new Favourite
                {
                    UserId = userId,
                    ArtworkId = artworkId,
                    CreatedAt = DateTime.Now
                };

                _context.Favourites.Add(favourite);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(
                "Details",
                "Artwork",
                new { id = artworkId });
        }

        // =========================
        // REMOVE FAVOURITE
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int artworkId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (!int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var favourite = await _context.Favourites
                .FirstOrDefaultAsync(f =>
                    f.UserId == userId &&
                    f.ArtworkId == artworkId);

            if (favourite != null)
            {
                _context.Favourites.Remove(favourite);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}