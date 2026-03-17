namespace CycloneLens.Models
{
    public class Cycloon
    {
        public int id { get; }
        public required string naam { get; set; }
        public StatusType status { get; set; }
        public int categorie { get; set; }
        public BassinType bassin { get; set; }
    }

    public enum StatusType
    {
        Actief,
        Inactief
    }

    public enum BassinType
    {
        Noord_Atlantisch,
        Oostelijk_Stille_Oceaan,
        Westelijk_Stille_Oceaan,
        Noordelijk_Indische_Oceaan,
        Zuidwest_Indische_Oceaan,
        Zuidoost_Indische_Oceaan,
        Westelijk_Australische_Oceaan,
        Zuidelijk_Stille_Oceaan
    }

    // + GetTraject(): List<Locatie>
}
