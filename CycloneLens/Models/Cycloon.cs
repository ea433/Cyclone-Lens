namespace CycloneLens.Models
{
    public class Cycloon
    {
        public int Id { get; }
        public string Naam { get; private set; }
        public StatusType Status { get; private set; }
        public int Categorie { get; set; }
        public BassinType Bassin { get; private set; }

        public Cycloon(int id, string naam, StatusType status, int categorie, BassinType bassin)
        {
            this.Id = id;
            this.Naam = naam;
            this.Status = status;
            this.Categorie = categorie;
            this.Bassin = bassin;
        }

        public enum StatusType
        {
            Actief,
            Inactief
        }

        public enum BassinType
        {
            Noord_Atlantisch
        }

        // + GetTraject(): List<Locatie>
        //public List<Locatie> GetTraject(float longitude, float latitude)
    }
}
    
