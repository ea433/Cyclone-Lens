using Logic.Enums;

namespace CycloneLens.Models
{
    public class Metadata
    {
        public int Id { get; }
        public int Cycloon_Id { get; } 
        public CategorieType Categorie { get; private set; }
        public double Windsnelheid { get; private set; }
        public double Luchtdruk { get; private set; } 
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public DateTime Tijdstip { get; private set; } 

        public Metadata(int id, int Cycloon_Id, CategorieType categorie, double windsnelheid, double luchtdruk, 
            double longitude, double latitude, DateTime tijdstip)
        {
            this.Id = id;
            this.Categorie = categorie;
            this.Windsnelheid = windsnelheid;
            this.Luchtdruk = luchtdruk;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Tijdstip = tijdstip;
        }
    }
}
