using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}