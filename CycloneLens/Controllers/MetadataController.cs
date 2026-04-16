using Microsoft.AspNetCore.Mvc;
using Interface_Layer.InterfaceRepositories;

namespace Presentation.Controllers
{
    public class MetadataController : Controller
    {
        private readonly ICycloonRepository _repository;
        private readonly ICycloonDataRepository _dataRepository;

        public MetadataController(ICycloonRepository repository, ICycloonDataRepository dataRepository)
        {
            _repository = repository;
            _dataRepository = dataRepository;
        }

        public IActionResult Details(int id)
        {
            var cyclonen = _repository.GetCyclonen();
            var metadata = _dataRepository.GetMetadata();

            var cycloon = cyclonen.First(c => c.Id == id);

            var traject = metadata
                .Where(m => m.Cycloon_Id == id)
                .OrderBy(m => m.Tijdstip)
                .ToList();

            return View((cycloon, traject));
        }
    }
}