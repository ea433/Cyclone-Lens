using Models.Classes;
using Data_Access_Layer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Types;
using Models.Enums;
using Presentation.Models;
using Business_Logic_Layer.Services;

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
            var dataRepository = new MetadataRepository(connectionString);
            var loggingRepository = new LoggingRepository(connectionString);
            _service = new CycloonService(repository, dataRepository, loggingRepository);
        }

        public IActionResult Index()
        {
            var data = _service.GetActiveCyclonenNATL();

            var ViewModel = data.Select(cycloon => new CycloonViewModel(
                cycloon.Id,
                cycloon.Naam,
                cycloon.Categorie,
                cycloon.Bassin,
                cycloon.Status
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

            var userTypeValue = HttpContext.Session.GetInt32("UserType");
            var userType = (UserType)(userTypeValue ?? 0);

            var gebruiker = new Gebruiker(
                userId ?? 0,
                "test",
                "test@test",
                "test",
                userType
            );

            if (string.IsNullOrWhiteSpace(model.Naam))
            {
                return View("Error");
            }

            // Example usage of enum
            if (gebruiker.UserType != UserType.Beheerder)
            {
                return View("Error"); // or Unauthorized()
            }

            var cycloon = new Cycloon(
                    model.Id,
                    model.Naam,
                    model.Status,
                    model.Bassin
                );

                var metadata = new Metadata(
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

        public IActionResult Satelliet()
        {
            return View();
        }
    }
}
  