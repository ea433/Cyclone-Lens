namespace CycloneLens.Models
{
    public class Gebruiker
    {
        public int Id { get; }
        public string Naam { get; private set; }
        private string Email { get; set; } 
        private string Wachtwoord { get; set; }
        public bool BeheerRechten { get; private set; } 

        public Gebruiker(int Id, string Naam, string Email, string Wachtwoord, bool BeheerRechten)
        {
            this.Id = Id;
            this.Naam = Naam;
            this.Email = Email;
            this.Wachtwoord = Wachtwoord;
            this.BeheerRechten = BeheerRechten;
        }

        // + UpdateCycloonData(string naam) : void
    }
}