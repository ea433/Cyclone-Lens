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

                SqlGeography coordinaten = SqlGeography.Point(vm.Latitude, vm.Longitude, 4326);

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

        [HttpGet]
        public IActionResult Index()
        {
            List<Observatie> observaties =
                _service.GetAllObservaties();

            List<ObservatieViewModel> viewModels = new();

            foreach (Observatie observatie in observaties)
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

        [HttpGet]
        public IActionResult Details(int id)
        {
            Observatie? observatie = _service.GetById(id);

            if (observatie == null)
            {
                return NotFound();
            }

            ObservatieViewModel viewModels = new ObservatieViewModel
            {
                Id = observatie.Id,
                Inzender = observatie.GebruikerNaam,
                Omschrijving = observatie.Omschrijving,
                Tijdstip = observatie.Tijdstip
            };

            return View(viewModels);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.DeleteObservatie(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Rapporteer(int observatieId)
        {
            try
            {
                int gebruikerId = 1;

                _service.RapporteerObservatie(gebruikerId, observatieId);

                TempData["Success"] =
                    "Observatie succesvol gerapporteerd.";
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetObservaties()
        {
            List<Observatie> observaties = _service.GetAllObservaties();

            return Json(observaties.Select(o => new
            {
                lat = o.Coordinaten?.Lat.Value,
                lng = o.Coordinaten?.Long.Value,
                description = o.Omschrijving,
                tijdstip = o.Tijdstip
            }));
        }
    }
}