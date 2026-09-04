using IzikoMuseumWebsite.Data;
using IzikoMuseumWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzikoMuseumWebsite.Controllers
{
    public class ArtworksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Artworks
        public async Task<IActionResult> Index()
        {
            var artworks = await _context.Artworks
                .Include(a => a.Artist)
                .ToListAsync();

            return View(artworks);
        }

        // GET: /Artworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artworks
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(a => a.ArtworkId == id);

            if (artwork == null)
            {
                return NotFound();
            }

            return View(artwork);
        }
    }
}