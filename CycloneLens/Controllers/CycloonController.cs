using CycloneLens.DAL;
using Microsoft.AspNetCore.Mvc;

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
            _service = new CycloonService(repository);
        }

        public IActionResult Index()
        {
            var cyclonen = _service.GetActiveCyclonenNATL();
            return View(cyclonen);
        }
    }
}
  