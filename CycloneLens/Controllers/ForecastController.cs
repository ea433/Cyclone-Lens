using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ForecastController : Controller
    {
        [HttpGet]
        public IActionResult UpdateForecast()
        {
            string json = @"
            [
              {
                ""lat"": 14,
                ""lng"": -30,
                ""forecastHour"": 0,
                ""category"": 0
              },
              {
                ""lat"": 16,
                ""lng"": -36,
                ""forecastHour"": 12,
                ""category"": 1
              },
              {
                ""lat"": 18,
                ""lng"": -42,
                ""forecastHour"": 24,
                ""category"": 2
              }
            ]";

            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "data",
                "jtwc_forecast.json"
            );

            System.IO.File.WriteAllText(path, json);

            return Content("Forecast updated.");
        }
    }
}
