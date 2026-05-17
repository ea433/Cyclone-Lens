using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Types;
using Models.Classes;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class ObservatieController : Controller
    {
        private readonly ObservatieService _service;

        public ObservatieController(ObservatieService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Create(int cycloonId)
        {
            return View(new ObservatieViewModel
            {
                CycloonId = cycloonId
            });
        }

        [HttpPost]
        public IActionResult Create(ObservatieViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                int gebruikerId = 1; // temp

                var coordinaten = SqlGeography.Point(vm.Latitude, vm.Longitude, 4326);

                _service.PlaatsObservatie(
                    gebruikerId,
                    vm.CycloonId,
                    vm.Omschrijving!,
                    coordinaten
                );

                TempData["Success"] = "Observatie succesvol toegevoegd!";
                return RedirectToAction("Index", "Cycloon");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult Index()
        {
            List<Observatie> observaties =
                _service.GetAllObservaties();

            List<ObservatieViewModel> viewModels = new();

            foreach (var observatie in observaties)
            {
                viewModels.Add(new ObservatieViewModel
                {
                    Id = observatie.Id,
                    Inzender = observatie.GebruikerNaam,
                    Tijdstip = observatie.Tijdstip,
                });
            }
            return View(viewModels);
        }
    }
}