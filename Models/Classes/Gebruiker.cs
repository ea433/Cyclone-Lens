namespace CycloneLens.Models
{
    public class Gebruiker
    {
        public int Id { get; }
        public string Naam { get; private set; }
        public string Email { get; private set; } 
        public string Wachtwoord { get; private set; }
        public bool BeheerRechten { get; private set; } 

        public Gebruiker(int id, string naam, string email, string wachtwoord, bool beheerRechten)
        {
            this.Id = id;
            this.Naam = naam ?? throw new ArgumentNullException(nameof(Naam));
            this.Email = email ?? throw new ArgumentNullException(nameof(email));
            this.Wachtwoord = wachtwoord ?? throw new ArgumentNullException(nameof(Wachtwoord));
            this.BeheerRechten = beheerRechten;
        }
    }
}