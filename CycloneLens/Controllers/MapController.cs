using Microsoft.AspNetCore.Mvc;
using Business_Logic_Layer.Services;
using Models.Classes;
using System.Text.Json;

namespace Presentation.Controllers
{
    /* pre-API
    
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
    
    */

    public class MapController : Controller
    {
        private readonly NhcStormService _service;
        private readonly HttpClient _http;

        public MapController(NhcStormService service, IHttpClientFactory httpClientFactory)
        {
            _service = service;
            _http = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                string url = "https://www.nhc.noaa.gov/CurrentStorms.json";
                string json = await _http.GetStringAsync(url);

                NhcResponse? response = JsonSerializer.Deserialize<NhcResponse>(json);

                if (response?.ActiveStorms != null)
                    _service.SyncFromNhc(response.ActiveStorms);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NHC sync fout: {ex.Message}");
            }

            return View();
        }
    }
}