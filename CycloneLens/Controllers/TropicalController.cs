using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Text;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TropicalController : ControllerBase
    {
        private static readonly HttpClient _http = new HttpClient();

        static TropicalController()
        {
            _http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        }

        // Active storms list — use directly from NHC
        [HttpGet("nhc-active")]
        public async Task<IActionResult> GetNhcActive()
        {
            var url = "https://www.nhc.noaa.gov/CurrentStorms.json";
            var response = await _http.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        // Fetches a KMZ zip, extracts the KML inside, returns it as text
        [HttpGet("kmz")]
        public async Task<IActionResult> GetKmz([FromQuery] string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("url required");

            var response = await _http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var zipBytes = await response.Content.ReadAsByteArrayAsync();

            try
            {
                using var zipStream = new System.IO.MemoryStream(zipBytes);
                using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);

                foreach (var entry in archive.Entries)
                {
                    if (entry.Name.EndsWith(".kml", StringComparison.OrdinalIgnoreCase))
                    {
                        using var reader = new System.IO.StreamReader(entry.Open(), Encoding.UTF8);
                        var kml = await reader.ReadToEndAsync();
                        return Content(kml, "application/xml");
                    }
                }

                return NotFound("No KML found in KMZ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ZIP error: {ex.Message}");
            }
        }

        [HttpGet("kmz-contents")]
        public async Task<IActionResult> GetKmzContents([FromQuery] string url)
        {
            var response = await _http.GetAsync(url, HttpCompletionOption.ResponseContentRead);

            var zipBytes = await response.Content.ReadAsByteArrayAsync();

            using var zipStream = new System.IO.MemoryStream(zipBytes);
            using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);

            var files = archive.Entries.Select(e => e.Name).ToList();
            return Ok(files);
        }
    }
}