using Data_Access_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Types;
using Models.Classes;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class ObservatieController : Controller
    {
        private readonly IObservatieRepository _repository;

        public ObservatieController(IObservatieRepository repository)
        {
            _repository = repository;
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

                if (string.IsNullOrWhiteSpace(vm.Omschrijving))
                {
                    ModelState.AddModelError("", "Omschrijving is verplicht");
                    return View(vm);
                }

                var coordinaten = SqlGeography.Point(vm.Latitude, vm.Longitude, 4326);

                var observatie = new ObservatieDTO
                {
                    GebruikerId = gebruikerId,
                    CycloonId = vm.CycloonId,
                    Omschrijving = vm.Omschrijving,
                    AfbeeldingPad = null,
                    Coordinaten = coordinaten,
                    Tijdstip = DateTime.Now
                };

                _repository.InsertObservatie(observatie);

                TempData["Success"] = "Observatie succesvol toegevoegd!";

                return RedirectToAction("Index", "Cycloon");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
    }
}