namespace CycloneLens.Models
{
    public class Gebruiker
    {
        public int Id { get; }
        public string Naam { get; private set; }
        private string Email { get; set; } 
        private string Wachtwoord { get; set; }
        public bool BeheerRechten { get; private set; } 

        public Gebruiker(int id, string naam, string email, string wachtwoord, bool beheerRechten)
        {
            this.Id = id;
            this.Naam = naam;
            this.Email = email;
            this.Wachtwoord = wachtwoord;
            this.BeheerRechten = beheerRechten;
        }

        // + UpdateCycloonData(string naam) : void
    }
}