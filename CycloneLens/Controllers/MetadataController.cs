using Microsoft.AspNetCore.Mvc;
using Business_Logic_Layer.Services;
using Models.Classes;

namespace Presentation.Controllers
{
    public class MetadataController : Controller
    {
        private readonly CycloonService _cycloonService;

        public MetadataController(CycloonService cycloonService)
        {
            _cycloonService = cycloonService;
        }

        public IActionResult Details(int id)
        {
            Cycloon? result = _cycloonService.GetCycloonDetails(id);

            if (result == null)
                return NotFound();

            return View(result);
        }
    }
}