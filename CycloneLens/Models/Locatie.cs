namespace CycloneLens.Models
{
    public class Locatie
    {
        public int Id { get; }
        public float Longitude { get; private set; }
        public float Latitude { get; private set; }
        public DateTime Tijdstip { get; private set; }

        public Locatie(int id, float longitude, float latitude, DateTime tijdstip)
        {
            this.Id = id;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Tijdstip = tijdstip;
        }

        public float GetCoordinates(float longitude, float latitude) // into service?
        {
            return longitude + latitude;
        }
    }
}

// + GetCoordinates(): (float, float)

