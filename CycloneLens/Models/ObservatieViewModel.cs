using Microsoft.SqlServer.Types;

namespace Presentation.Models
{
    public class ObservatieViewModel
    {
        public int CycloonId { get; set; }
        public string? Omschrijving { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
