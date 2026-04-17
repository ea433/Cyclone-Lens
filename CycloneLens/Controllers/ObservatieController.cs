using Interface_Layer.InterfaceRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Types;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class ObservatieController : Controller
    {
        private readonly IObservatieService _service;

        public ObservatieController(IObservatieService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Create(int cycloonId)
        {
            return View();
            /*return View(new ObservatieViewModel
            {
                CycloonId = cycloonId
            });*/
        }

        [HttpPost]
        public IActionResult Create(ObservatieViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                // TEMP: hardcoded user (replace later if needed)
                int gebruikerId = 1;

                var coordinaten = SqlGeography.Point(vm.Latitude, vm.Longitude, 4326);

                if (string.IsNullOrWhiteSpace(vm.Omschrijving))
                {
                    ModelState.AddModelError("", "Omschrijving is verplicht");
                    return View(vm);
                }

                _service.PlaatsObservatie(
                    gebruikerId,
                    vm.CycloonId,
                    vm.Omschrijving!,
                    coordinaten
                );

                return RedirectToAction("Details", "Cycloon", new { id = vm.CycloonId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
    }
}
