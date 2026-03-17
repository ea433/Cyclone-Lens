using System.ComponentModel.DataAnnotations;

namespace CycloneLens.Models
{
    public class Locatie
    {
        public float longitude { get; set; } 
        public float latitude { get; set; } 
        public DateTime tijdstip { get; set; } 
    }

    // + GetCoordinates(): (float, float)
}
