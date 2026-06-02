using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoesProxyController : ControllerBase
    {
        private static readonly HttpClient _http = new HttpClient();

        static GoesProxyController()
        {
            _http.DefaultRequestHeaders.Add("Referer", "https://nowcoast.noaa.gov/");
            _http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        }

        [HttpGet("tile")]
        public async Task<IActionResult> GetTile(
            [FromQuery] string minx,
            [FromQuery] string miny,
            [FromQuery] string maxx,
            [FromQuery] string maxy,
            [FromQuery] int width = 256,
            [FromQuery] int height = 256,
            [FromQuery] string layers = "global_visible_imagery_mosaic,global_longwave_imagery_mosaic")
        {
            // Parse the values so we can expand the bbox slightly
            if (!double.TryParse(minx, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out double dMinX) ||
                !double.TryParse(miny, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out double dMinY) ||
                !double.TryParse(maxx, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out double dMaxX) ||
                !double.TryParse(maxy, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out double dMaxY))
            {
                return BadRequest("Invalid bbox");
            }

            // Expand bbox by 1% on each side to cover tile edge gaps
            double bufferX = (dMaxX - dMinX) * 0.01;
            double bufferY = (dMaxY - dMinY) * 0.01;

            var wmsUrl =
                $"https://nowcoast.noaa.gov/geoserver/observations/satellite/ows" +
                $"?SERVICE=WMS&VERSION=1.3.0&REQUEST=GetMap" +
                $"&LAYERS={layers}" +
                $"&BBOX={dMinX - bufferX},{dMinY - bufferY},{dMaxX + bufferX},{dMaxY + bufferY}" +
                $"&WIDTH={width}&HEIGHT={height}" +
                $"&CRS=EPSG:3857" +
                $"&FORMAT=image/png" +
                $"&TRANSPARENT=true";

            try
            {
                var response = await _http.GetAsync(wmsUrl);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode);

                var contentType = response.Content.Headers.ContentType?.ToString() ?? "image/png";
                var bytes = await response.Content.ReadAsByteArrayAsync();

                Response.Headers["Cache-Control"] = "public, max-age=240";
                return File(bytes, contentType);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}