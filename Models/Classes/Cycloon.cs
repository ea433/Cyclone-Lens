using Logic.Enums;

namespace CycloneLens.Models
{
    public class Cycloon
    {
        public int Id { get; }
        public string Naam { get; private set; }
        public CategorieType Categorie { get; private set; } 
        public StatusType Status { get; private set; }
        public BassinType Bassin { get; private set; }
        public List<Metadata> Metadata { get; private set; } = new();

        public Cycloon(int id, string naam, CategorieType categorie, StatusType status, BassinType bassin, List<Metadata> metadata)
        {
            this.Id = id;
            this.Naam = naam ?? throw new ArgumentNullException(nameof(naam));
            this.Categorie = categorie;
            this.Status = status;
            this.Bassin = bassin;
            this.Metadata = metadata;
        }

        public Cycloon(int id, string naam, StatusType status, BassinType bassin)
        {
            this.Id = id;
            this.Naam = naam ?? throw new ArgumentNullException(nameof(naam));
            this.Status = status;
            this.Bassin = bassin;
        }
    }
}
    
