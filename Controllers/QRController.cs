using IzikoMuseumWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzikoMuseumWebsite.Controllers
{
    public class QRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QRController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // QR LOOKUP PAGE
        // =========================

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // =========================
        // SEARCH QR CODE
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Lookup(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                ViewBag.Error = "Please enter a QR artwork code.";
                return View("Index");
            }

            code = code.Trim();

            var qrCode = await _context.QRCodes
                .Include(q => q.Artwork)
                .ThenInclude(a => a!.Artist)
                .Include(q => q.Artwork)
                .ThenInclude(a => a!.Category)
                .Include(q => q.Artwork)
                .ThenInclude(a => a!.Gallery)
                .FirstOrDefaultAsync(q => q.Code == code);

            if (qrCode == null || qrCode.Artwork == null)
            {
                ViewBag.Error =
                    "No artwork was found for this QR code.";

                return View("Index");
            }

            return RedirectToAction(
                "Details",
                "Artwork",
                new { id = qrCode.ArtworkId });
        }
    }
}