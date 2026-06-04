using Models.Classes;
using Data_Access_Layer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Types;
using Models.Enums;
using Presentation.Models;
using Business_Logic_Layer.Services;

namespace Presentation.Controllers
{
    public class CycloonController : Controller
    {
        private readonly CycloonService _service;

        public CycloonController(IConfiguration config)
        {
            string? connectionString = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string not found");
            }

            CycloonRepository repository = new CycloonRepository(connectionString);
            MetadataRepository dataRepository = new MetadataRepository(connectionString);
            LoggingRepository loggingRepository = new LoggingRepository(connectionString);
            _service = new CycloonService(repository, dataRepository, loggingRepository);
        }

        public IActionResult Index()
        {
            List<Cycloon> data = _service.GetActiveCyclonenNATL();

            List<CycloonViewModel> viewModel = data.Select(cycloon => new CycloonViewModel(
                cycloon.Id,
                cycloon.Naam,
                cycloon.Categorie,
                cycloon.Bassin,
                cycloon.Status
            )).ToList();

            return View(viewModel);
        }

    [HttpPost]
    public IActionResult Update(UpdateCycloonViewModel model)
    {
        try
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            int? userTypeValue = HttpContext.Session.GetInt32("UserType");
            GebruikerType userType = (GebruikerType)(userTypeValue ?? 0);

            Gebruiker gebruiker = new Gebruiker(
                userId ?? 0,
                "test",
                "test",
                userType
            );

            if (string.IsNullOrWhiteSpace(model.Naam))
            {
                return View("Error");
            }

            // Example usage of enum
            if (gebruiker.UserType != GebruikerType.Beheerder)
            {
                return View("Error"); // or Unauthorized()
            }

            Cycloon cycloon = new Cycloon(
                    model.Id,
                    model.Naam,
                    model.Status,
                    model.Bassin
                );

            Metadata metadata = new Metadata(
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
            Cycloon? cycloon = _service.GetById(id);

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
  