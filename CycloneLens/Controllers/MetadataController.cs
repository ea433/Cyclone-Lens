using Microsoft.AspNetCore.Mvc;
using Interface_Layer.InterfaceRepositories;

namespace Presentation.Controllers
{
    public class MetadataController : Controller
    {
        private readonly ICycloonRepository _repository;
        private readonly IMetadataRepository _dataRepository;
        private readonly CycloonService _cycloonService;

        public MetadataController(ICycloonRepository repository, IMetadataRepository dataRepository, CycloonService cycloonService)
        {
            _repository = repository;
            _dataRepository = dataRepository;
            _cycloonService = cycloonService;
        }

        public IActionResult Details(int id)
        {
            var result = _cycloonService.GetCycloonDetails(id);

            if (result == null)
                return NotFound();

            return View(result.Value);
        }
    }
}