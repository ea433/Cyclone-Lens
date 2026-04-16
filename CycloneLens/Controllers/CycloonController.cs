using CycloneLens.Models;
using Data_Access_Layer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Types;
using Presentation.Models;

namespace CycloneLens.Controllers
{
    public class CycloonController : Controller
    {
        private readonly CycloonService _service;

        public CycloonController(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string not found");
            }

            var repository = new CycloonRepository(connectionString);
            var dataRepository = new CycloonDataRepository(connectionString);
            _service = new CycloonService(repository, dataRepository);
        }

        public IActionResult Index()
        {
            var data = _service.GetActiveCyclonenNATL();

            var ViewModel = data.Select(c => new ViewModel(
                c.Id,
                c.Naam,
                c.Categorie,
                c.Bassin,
                c.Status
            )).ToList();

            return View(ViewModel);
        }

        // fr-05
        [HttpPost]
        public IActionResult Update(UpdateCycloonViewModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var isAdmin = HttpContext.Session.GetInt32("IsAdmin") == 1;

                // var gebruiker = new Gebruiker(userId ?? 0, "temp", "temp@test", "temp", isAdmin);
                var gebruiker = new Gebruiker(1, "test", "test@test", "test", true);

                if (string.IsNullOrWhiteSpace(model.Naam))
                {
                    return View("Error");
                }

                var cycloon = new Cycloon(
                    model.Id,
                    model.Naam,
                    model.Status,
                    model.Bassin
                );

                var metadata = new CycloonData(
                    0,
                    model.Id,
                    model.Categorie,
                    model.Windsnelheid,
                    model.Luchtdruk,
                    SqlGeography.Point(model.Coordinaten, model.Coordinaten, 4326),
                    DateTime.UtcNow
                );

                _service.UpdateCycloon(cycloon, metadata, gebruiker);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public IActionResult Edit(int id)
        {
            var cycloon = _service.GetById(id);

            if (cycloon == null)
                return View("Error");

            return View(cycloon);
        }

        public IActionResult CycloonSatellietView()
        {
            return View();
        }
    }
}
  