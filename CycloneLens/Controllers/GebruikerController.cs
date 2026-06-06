using Business_Logic_Layer.Services;
using Data_Access_Layer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.Classes;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class GebruikerController : Controller
    {
        private readonly GebruikerService _service;

        public GebruikerController(GebruikerService service)
        {
            _service = service;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(GebruikerViewModel model)
        {
            try
            {
                _service.RegistreerGebruiker(model.Gebruikersnaam, model.Wachtwoord);
                return RedirectToAction("Login");
            }
            catch (ArgumentException ex)
            {
                ViewBag.Fout = ex.Message;
                return View(model);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(GebruikerViewModel model)
        {
            Gebruiker? gebruiker = _service.Login(model.Gebruikersnaam, model.Wachtwoord);

            if (gebruiker == null)
            {
                ViewBag.Fout = "Onjuiste gebruikersnaam of wachtwoord.";
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", gebruiker.Id);
            HttpContext.Session.SetInt32("UserType", (int)gebruiker.UserType);
            return RedirectToAction("Index", "Cycloon");
        }
    }
}