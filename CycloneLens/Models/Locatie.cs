namespace CycloneLens.Models
{
    public class Locatie
    {
        public int id { get; }
        public float longitude { get; private set; }
        public float latitude { get; private set; }
        public DateTime tijdstip { get; }

        public Locatie(int id, float longitude, float latitude, DateTime tijdstip)
        {
            this.id = id;
            this.longitude = longitude;
            this.latitude = latitude;
            this.tijdstip = tijdstip;
        }

        public float GetCoordinates(float longitude, float latitude)
        {
            return longitude + latitude;
        }
    }
}

        // + GetCoordinates(): (float, float)

