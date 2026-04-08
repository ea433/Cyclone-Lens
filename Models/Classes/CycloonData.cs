using Logic.Enums;
using Microsoft.SqlServer.Types;

namespace CycloneLens.Models
{
    public class CycloonData
    {
        public int Id { get; }
        public int Cycloon_Id { get; } 
        public CategorieType Categorie { get; private set; }
        public double Windsnelheid { get; private set; }
        public double Luchtdruk { get; private set; } 
        public SqlGeography Coordinaten { get; private set; }
        public DateTime Tijdstip { get; private set; } 

        public CycloonData(int id, int Cycloon_Id, CategorieType categorie, double windsnelheid, double luchtdruk, 
            SqlGeography coordinaten, DateTime tijdstip)
        {
            this.Id = id;
            this.Cycloon_Id = Cycloon_Id;
            this.Categorie = categorie;
            this.Windsnelheid = windsnelheid;
            this.Luchtdruk = luchtdruk;
            this.Coordinaten = coordinaten;
            this.Tijdstip = tijdstip;
        }
    }
}
