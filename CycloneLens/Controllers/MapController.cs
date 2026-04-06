using Microsoft.AspNetCore.Mvc;

namespace CycloneLens.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}