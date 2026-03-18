namespace CycloneLens.Models
{
    public class Gebruiker
    {
        public int id { get; }
        public string naam { get; private set; }
        private string email { get; set; } 
        private string wachtwoord { get; set; }
        public bool beheerRechten { get; set; } 

        public Gebruiker(int id, string naam, string email, string wachtwoord, bool beheerRechten)
        {
            this.id = id;
            this.naam = naam;
            this.email = email;
            this.wachtwoord = wachtwoord;
            this.beheerRechten = beheerRechten;
        }

        // + UpdateCycloonData(c: Cycloon) : void
    }
}