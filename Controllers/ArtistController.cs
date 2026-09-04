using IzikoMuseumWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzikoMuseumWebsite.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Artist
        public async Task<IActionResult> Index()
        {
            var artists = await _context.Artists
                .Include(a => a.Artworks)
                .ToListAsync();

            return View(artists);
        }

        // GET: /Artist/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .Include(a => a.Artworks)
                .FirstOrDefaultAsync(a => a.ArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }
    }
}