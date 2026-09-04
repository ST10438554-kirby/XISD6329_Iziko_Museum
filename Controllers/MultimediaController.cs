using Microsoft.AspNetCore.Mvc;

namespace IzikoMuseumWebsite.Controllers
{
    public class MultimediaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}