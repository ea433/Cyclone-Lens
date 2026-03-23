using Microsoft.AspNetCore.Mvc;

namespace SatelliteController
{
    public class SatelliteController : Controller
    {
        public IActionResult Index()
        { 
            string date = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");

            string url = $"https://gibs.earthdata.nasa.gov/wms/epsg4326/best/wms.cgi?" +
                         $"SERVICE=WMS&REQUEST=GetMap&VERSION=1.1.1" +
                         $"&LAYERS=MODIS_Terra_CorrectedReflectance_TrueColor" +
                         $"&FORMAT=image/jpeg&HEIGHT=500&WIDTH=900&SRS=EPSG:4326" +
                         $"&BBOX=-100,0,10,50" +
                         $"&TIME={date}" +
                         $"&cb={DateTime.UtcNow.Ticks}";

            ViewBag.ImageUrl = url;
            ViewBag.TitleText = "Cyclone Satellite Imagery";
            ViewBag.Date = date;

            return View();
        }
    }
}