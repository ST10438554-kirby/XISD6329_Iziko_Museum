using Microsoft.AspNetCore.Mvc;

namespace IzikoMuseumWebsite.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}